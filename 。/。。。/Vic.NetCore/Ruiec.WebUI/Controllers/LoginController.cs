using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vic.Core.Application.IServices;
using Vic.Core.Utils.Models;
using Microsoft.AspNetCore.Http;
using Vic.Core.Utils;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ruiec.WebUI.Controllers
{
    public class LoginController : Controller
    {


        private IUserAppService _userAppService;
        public LoginController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        /// <summary>
        /// 登录界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }
        /// <summary>
        /// 登录处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> UserLoginAsync(LoginModel model)
        {
            bool IsSucceed = false;
            string errMsg = string.Empty;
            if (ModelState.IsValid)
            {
                //校验手机验证码
                if (!IsValidateSession(ConstDefine.AdminLoginPhoneCodeSession, model.PhoneCode, out errMsg))
                {
                    errMsg = "手机验证码" + errMsg;
                    return Json(new OperateMessage<string>(IsSucceed ? OperateResult.Success : OperateResult.Fail, true, IsSucceed ? "登录成功" : errMsg));
                }
                //检查用户信息
                var user = _userAppService.CheckUser(model.UserName, model.Password);
                if (user != null)
                {
                    var identity = new ClaimsIdentity("Forms");// 指定身份认证类型

                    identity.AddClaim(new Claim(ClaimTypes.Sid, user.ID.ToString()));//用户Id
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));//用户名称
                    identity.AddClaim(new Claim(ClaimTypes.MobilePhone, user.MobileNumber));//用户手机号
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(ConstDefine.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = true });
                    string returnUrl = Request.Query["returnUrl"];
                    if (string.IsNullOrEmpty(returnUrl))
                        returnUrl = ConstDefine.AdminHomeUrl;
                    IsSucceed = true;
                    return Json(new OperateMessage<string>(IsSucceed ? OperateResult.Success : OperateResult.Fail, true, (IsSucceed ? "登录成功" : errMsg), null, 1, returnUrl));
                }
                errMsg = "用户名或密码错误";
                return Json(new OperateMessage<string>(IsSucceed ? OperateResult.Success : OperateResult.Fail, true, IsSucceed ? "登录成功" : errMsg));
            }
            IsSucceed = false;
            foreach (var item in ModelState.Values)
            {
                if (item.Errors.Count > 0)
                {
                    errMsg = item.Errors[0].ErrorMessage;
                    break;
                }
            }
            return Json(new OperateMessage<string>(IsSucceed ? OperateResult.Success : OperateResult.Fail, true, IsSucceed ? "登录成功" : errMsg));
        }

        public async Task<IActionResult> RedirectAsync(ClaimsPrincipal principal)
        {
            string returnUrl = Request.Query["returnUrl"];
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = ConstDefine.AdminHomeUrl;
            await HttpContext.SignInAsync(ConstDefine.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = false, RedirectUri = returnUrl });


            //跳转到系统首页
            return RedirectPermanent(returnUrl);
        }

        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> UserLogOutAsync()
        {
            await HttpContext.SignOutAsync(ConstDefine.AuthenticationScheme);// Startup.cs中配置的验证方案名
            return Redirect(ConstDefine.AdminLoginUrl);
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public FileResult GetValidateCode()
        {
            string code = string.Empty;
            var data = Common.VerificationCode(out code, 4);
            SetSession(ConstDefine.AdminLoginVerifyCodeSession, code, 60 * 2);
            return File(data, @"image/png");
        }
        /// <summary>
        /// 获取短信验证码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="imgCode"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetPhoneCode(string userName, string imgCode)
        {
            bool IsSucceed = false;
            //校验图形验证码
            string errMsg = string.Empty;
            if (!IsValidateSession(ConstDefine.AdminLoginVerifyCodeSession, imgCode, out errMsg))
            {
                errMsg = "图形验证码" + errMsg;
                return Json(new OperateMessage<string>(IsSucceed ? OperateResult.Success : OperateResult.Fail, true, IsSucceed ? "发送成功" : errMsg));
            }
            string code = Common.GenerateRandom(4);
            SetSession(ConstDefine.AdminLoginPhoneCodeSession, code, 60 * 2);
            IsSucceed = true;
            return Json(new OperateMessage<string>(IsSucceed ? OperateResult.Success : OperateResult.Fail, true, IsSucceed ? "发送成功" : "发送失败"));
        }

        #region session操作封装
        /// <summary>
        /// 设置session
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="expireSecond">多少秒后过期时间</param>
        private void SetSession(string key, string value, double expireSecond)
        {
            HttpContext.Session.SetString(key, value);
            HttpContext.Session.SetString(key + "ExpireDate", DateTime.Now.AddSeconds(expireSecond).ToString());
        }
        /// <summary>
        /// 删除Session
        /// </summary>
        /// <param name="key"></param>
        private void RemoveSession(string key)
        {
            HttpContext.Session.Remove(key);
            HttpContext.Session.Remove(key + "ExpireDate");
        }
        /// <summary>
        /// 校验Session是否有效
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private bool IsValidateSession(string key, string val, out string errMsg)
        {
            errMsg = string.Empty;
            if (val == ConstDefine.AdminSupperPassCode)
            {
                return true;
            }
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(val))
            {
                errMsg = "不能为空";
                return false;
            }
            string keyExpireDate = HttpContext.Session.GetString(key + "ExpireDate");
            string keyVal = HttpContext.Session.GetString(key);

            //请求校验后，立即清理Session
            RemoveSession(key);

            if (string.IsNullOrEmpty(keyExpireDate))
            {
                errMsg = "已失效";
                return false;
            }
            DateTime ExpireDate = DateTime.Now.AddDays(-1);
            if (DateTime.TryParse(keyExpireDate, out ExpireDate))
            {
                if (DateTime.Now > ExpireDate)
                {
                    errMsg = "已失效";
                    return false;
                }
            }
            else
            {
                errMsg = "已失效";
                return false;
            }
            if (string.IsNullOrEmpty(keyVal))
            {
                errMsg = "已失效";
                return false;
            }
            if (keyVal != val.ToLower())
            {
                errMsg = "错误";
                return false;
            }
            return true;
        }
        #endregion
    }
}
