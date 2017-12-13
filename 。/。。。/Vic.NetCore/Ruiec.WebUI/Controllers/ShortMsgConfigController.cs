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
    public class ShortMsgConfigController : BaseController
    {
        #region 数据接口
        private readonly IShortMsgConfigAppService _ShortMsgConfigAppService;
        #endregion

        #region 构造器
        public ShortMsgConfigController(IShortMsgConfigAppService ShortMsgConfigAppService)
        {
            _ShortMsgConfigAppService = ShortMsgConfigAppService;
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
            PageDataResponse<ShortMsgConfigDto> pageData = new PageDataResponse<ShortMsgConfigDto>();
            var parameterExpression = System.Linq.Expressions.Expression.Parameter(typeof(ShortMsgConfig));
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
            var queryLambdaFilter = System.Linq.Expressions.Expression.Lambda<Func<ShortMsgConfig, bool>>(filter, parameterExpression);
            int Count;
            List<ShortMsgConfigDto> List = _ShortMsgConfigAppService.PageList(queryLambdaFilter, pageParams.PageSize, pageParams.CurPage, out Count);
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
            return View();
        }

        /// <summary>
        /// 保存新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Create(ShortMsgConfigDto model)
        {
            string errs = GetModelStateError();
            if (!string.IsNullOrEmpty(errs))
            {
                return Json(new OperateMessage<string>(OperateResult.Fail, true, errs));
            }
            bool IsSucceed = _ShortMsgConfigAppService.Create(new ShortMsgConfigDto()
            {
                ID = Guid.NewGuid(),
                			AccountName=model.AccountName,
			AccountPwd=model.AccountPwd,
			ApiKey=model.ApiKey,
			ApiSecret=model.ApiSecret,
			ApiUrl=model.ApiUrl,
			CallEnumKey=model.CallEnumKey,
			CreateTime=DateTime.Now,
			CreatorID=_currUser.ID,
			IsDefault=model.IsDefault,
			LastUpdateTime=DateTime.Now,
			LastUpdateUserID=_currUser.ID,

            });
            if (IsSucceed)
            {
                return Json(new OperateMessage<string>("/ShortMsgConfig/Index"));
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
            ShortMsgConfigDto currModel = _ShortMsgConfigAppService.Get(ID);
            return View(currModel);
        }

        /// <summary>
        /// 保存编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(ShortMsgConfigDto model)
        {
            string errs = GetModelStateError();
            if (!string.IsNullOrEmpty(errs))
            {
                return Json(new OperateMessage<string>(OperateResult.Fail, true, errs));
            }
            ShortMsgConfigDto currModel = _ShortMsgConfigAppService.Get(model.ID);
            if (currModel == null)
            {
                return Json(new OperateMessage<string>(OperateResult.Fail, true, "修改失败，修改的内容不存在！"));
            }
            			currModel.AccountName=model.AccountName;
			currModel.AccountPwd=model.AccountPwd;
			currModel.ApiKey=model.ApiKey;
			currModel.ApiSecret=model.ApiSecret;
			currModel.ApiUrl=model.ApiUrl;
			currModel.CallEnumKey=model.CallEnumKey;
			currModel.IsDefault=model.IsDefault;
			currModel.LastUpdateTime=DateTime.Now;
			currModel.LastUpdateUserID=_currUser.ID;

            bool IsSucceed = _ShortMsgConfigAppService.Edit(currModel);
            if (IsSucceed)
            {
                return Json(new OperateMessage<string>("/ShortMsgConfig/Index"));
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
            bool IsSucceed = _ShortMsgConfigAppService.DeleteBatch(delIds);
            return Json(new { ret = IsSucceed ? "0000" : "-999", msg = IsSucceed ? "删除成功！" : "删除失败，请先删除外键关联记录！" });
        }
        #endregion
    }
}