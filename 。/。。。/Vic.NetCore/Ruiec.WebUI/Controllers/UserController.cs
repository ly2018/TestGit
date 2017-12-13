using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vic.Core.Application.Dtos;
using Vic.Core.Application.IServices;
using Vic.Core.Domain.Entities;
using Vic.Core.Utils;
using Vic.Core.Utils.RequestParameters;
using Vic.Core.Utils.ResponseData;

namespace Ruiec.WebUI.Controllers
{
    public class UserController : BaseController
    {
        #region 数据接口
        private readonly IUserAppService _UserAppService;
        private readonly IDepartmentAppService _departmentAppService;
        #endregion

        #region 构造器
        public UserController(IUserAppService UserAppService, IDepartmentAppService departmentAppService)
        {
            _departmentAppService = departmentAppService;
            _UserAppService = UserAppService;
        }
        #endregion

        #region 首页
        /// <summary>
        /// 首页列表分页及模糊查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(PageParameter pageParams)
        {
            PageDataResponse<UserDto> pageData = new PageDataResponse<UserDto>();
            var parameterExpression = System.Linq.Expressions.Expression.Parameter(typeof(User));
            var filter = (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Constant(true);
            if (!string.IsNullOrEmpty(pageParams.Key))
            {
                pageParams.Key = pageParams.Key.Trim();
                filter = (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Constant(false);
                Guid guid = Guid.Empty;
                if (Guid.TryParse(pageParams.Key, out guid))
                    filter = filter.GotoOrElse(parameterExpression.GotoEqual("ID", guid));
            }
            else
            {
                filter = filter.GotoOrElse(parameterExpression.GotoNotEqual("ID", Guid.Empty));
            }
            if (pageParams.BeginTime != null && pageParams.EndTime != null && pageParams.BeginTime <= pageParams.EndTime)
            {
                filter = filter.GotoAndAlso(parameterExpression.GotoGreaterThanByDateTime("CreateTime", pageParams.BeginTime));
                filter = filter.GotoAndAlso(parameterExpression.GotoLessThanByDateTime("CreateTime", pageParams.EndTime));
            }
            filter = filter.GotoAndAlso(parameterExpression.GotoNotEqual("ID", ConstDefine.SuperAdminID));
            var queryLambdaFilter = System.Linq.Expressions.Expression.Lambda<Func<User, bool>>(filter, parameterExpression);
            int Count;
            List<UserDto> List = _UserAppService.PageList(queryLambdaFilter, pageParams.PageSize, pageParams.CurPage, out Count);
            if (List.Any())
            {
                var depIds = List.Select(a => a.DepartmentId).ToArray();
                var depList = _departmentAppService.QueryList(c => depIds.Contains(c.ID));
                foreach (var item in List)
                {
                    var temp = depList.FirstOrDefault(a => a.ID == item.DepartmentId);
                    if (temp != null)
                        item.DepartmentName = temp.Name;
                }
            }
            pageData.Data = List;
            pageData.TotalItem = Count;
            pageData.QueryData = pageParams;
            pageData.PageHTML = base.PageHtml(Count, pageParams.CurPage, pageParams.PageSize, this.HttpContext);
            return View(pageData);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.DepList = _departmentAppService.QueryList(c => c.ID != Guid.Empty);
            return View();
        }

        /// <summary>
        /// 保存新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Create(UserDto model)
        {
            string errs = GetModelStateError();
            if (!string.IsNullOrEmpty(errs))
            {
                return Json(new OperateMessage<string>(OperateResult.Fail, true, errs));
            }
            bool IsSucceed = _UserAppService.Create(new UserDto()
            {
                ID = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                CreatorID = _currUser.ID,
                DepartmentId = model.DepartmentId,
                EMail = model.EMail,
                IsDeleted = model.IsDeleted,
                LastLoginTime = DateTime.Now,
                LastUpdateTime = DateTime.Now,
                LastUpdateUserID = _currUser.ID,
                LoginTimes = model.LoginTimes,
                MobileNumber = model.MobileNumber,
                Name = model.Name,
                Password =Vic.Core.Utils.SecurityHelper.EncryptDES(model.Password), //进行加密,
                Remarks = model.Remarks,
                UserName = model.UserName,

            });
            if (IsSucceed)
            {
                return Json(new OperateMessage<string>("/User/Index"));
            }
            return Json(new OperateMessage<string>(IsSucceed ? OperateResult.Success : OperateResult.Fail, true, IsSucceed ? "添加成功！" : "添加失败！"));

        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(Guid ID)
        {
            ViewBag.DepList = _departmentAppService.QueryList(c => c.ID != Guid.Empty);
            UserDto currModel = _UserAppService.Get(ID);
            return View(currModel);
        }

        /// <summary>
        /// 保存编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(UserDto model)
        {
            string errs = GetModelStateError();
            if (!string.IsNullOrEmpty(errs))
            {
                return Json(new OperateMessage<string>(OperateResult.Fail, true, errs));
            }
            UserDto currModel = _UserAppService.Get(model.ID);
            if (currModel == null)
            {
                return Json(new OperateMessage<string>(OperateResult.Fail, true, "修改失败，修改的内容不存在！"));
            }
            currModel.DepartmentId = model.DepartmentId;
            currModel.EMail = model.EMail;
            currModel.IsDeleted = model.IsDeleted;
            currModel.LastLoginTime = DateTime.Now;
            currModel.LastUpdateTime = DateTime.Now;
            currModel.LastUpdateUserID = _currUser.ID;
            currModel.LoginTimes = model.LoginTimes;
            currModel.MobileNumber = model.MobileNumber;
            currModel.Name = model.Name;
            currModel.Remarks = model.Remarks;
            currModel.UserName = model.UserName;

            bool IsSucceed = _UserAppService.Edit(currModel);
            if (IsSucceed)
            {
                return Json(new OperateMessage<string>("/User/Index"));
            }
            return Json(new OperateMessage<string>(IsSucceed ? OperateResult.Success : OperateResult.Fail, true, IsSucceed ? "修改成功！" : "修改失败！"));
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(string Ids)
        {
            string[] arr = Ids.Split(',').ToArray();
            if (arr.Length <= 0)
            {
                return Json(new { ret = "-999", msg = "请选择要删除的记录！" });
            }
            List<Guid> delIds = new List<Guid>();
            foreach (var item in arr)
            {
                delIds.Add(Guid.Parse(item));
            }
            bool IsSucceed = _UserAppService.DeleteBatch(delIds);
            return Json(new { ret = IsSucceed ? "0000" : "-999", msg = IsSucceed ? "删除成功！" : "删除失败，请先删除外键关联记录！" });
        }
        #endregion
    }
}