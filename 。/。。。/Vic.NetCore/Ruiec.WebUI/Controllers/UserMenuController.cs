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
    public class UserMenuController : BaseController
    {
        #region 数据接口
        private readonly IUserMenuAppService _UserMenuAppService;
        private readonly ILanguageTypeAppService _languageTypeAppService;
        #endregion

        #region 构造器
        public UserMenuController(ILanguageTypeAppService languageTypeAppService, IUserMenuAppService UserMenuAppService)
        {
            _languageTypeAppService = languageTypeAppService;
            _UserMenuAppService = UserMenuAppService;
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
            PageDataResponse<UserMenuDto> pageData = new PageDataResponse<UserMenuDto>();
            var parameterExpression = System.Linq.Expressions.Expression.Parameter(typeof(UserMenu));
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
            var queryLambdaFilter = System.Linq.Expressions.Expression.Lambda<Func<UserMenu, bool>>(filter, parameterExpression);
            int Count;
            List<UserMenuDto> List = _UserMenuAppService.PageList(queryLambdaFilter, pageParams.PageSize, pageParams.CurPage, out Count);
            if (List.Any())
            {
                Guid[] langIds = List.Select(a => a.LanguageTypeID).Distinct().ToArray();
                var langList = _languageTypeAppService.QueryList(a => langIds.Contains(a.ID));
                Guid[] parentIds = List.Where(a => a.ParentId != Guid.Empty).Select(a => a.ID).ToArray();
                var parentList = _UserMenuAppService.QueryList(a => parentIds.Contains(a.ID));
                foreach (var item in List)
                {
                    var temp = langList.FirstOrDefault(a => a.ID.Equals(item.LanguageTypeID));
                    if (temp != null)
                    {
                        item.LanguageTypeName = string.Format("{0}/{1}", temp.Code, temp.Title);
                    }
                    if (item.ParentId == Guid.Empty) continue;
                    var parent = parentList.FirstOrDefault(a => a.ID == item.ParentId);
                    if (parent != null)
                    {
                        item.ParentName = parent.Name;
                    }
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
            ViewBag.ParentList = _UserMenuAppService.QueryList(a => a.ParentId== Guid.Empty);
            ViewBag.LangList = _languageTypeAppService.QueryList(a => a.ID != Guid.Empty);
            return View();
        }

        /// <summary>
        /// 保存新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Create(UserMenuDto model)
        {
            string errs = GetModelStateError();
            if (!string.IsNullOrEmpty(errs))
            {
                return Json(new OperateMessage<string>(OperateResult.Fail, true, errs));
            }
            bool IsSucceed = _UserMenuAppService.Create(new UserMenuDto()
            {
                ID = Guid.NewGuid(),
                Code = model.Code,
                CreateTime = DateTime.Now,
                CreatorID = _currUser.ID,
                Icon = model.Icon,
                LanguageTypeID = model.LanguageTypeID,
                LastUpdateTime = DateTime.Now,
                LastUpdateUserID = _currUser.ID,
                Name = model.Name,
                ParentId = model.ParentId,
                Remarks = model.Remarks,
                SerialNumber = model.SerialNumber,
                Type = model.Type,
                Url = model.Url,

            });
            if (IsSucceed)
            {
                return Json(new OperateMessage<string>("/UserMenu/Index"));
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
            ViewBag.ParentList = _UserMenuAppService.QueryList(a => a.ParentId == Guid.Empty);
            ViewBag.LangList = _languageTypeAppService.QueryList(a => a.ID != Guid.Empty);
            UserMenuDto currModel = _UserMenuAppService.Get(ID);
            return View(currModel);
        }

        /// <summary>
        /// 保存编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(UserMenuDto model)
        {
            string errs = GetModelStateError();
            if (!string.IsNullOrEmpty(errs))
            {
                return Json(new OperateMessage<string>(OperateResult.Fail, true, errs));
            }
            UserMenuDto currModel = _UserMenuAppService.Get(model.ID);
            if (currModel == null)
            {
                return Json(new OperateMessage<string>(OperateResult.Fail, true, "修改失败，修改的内容不存在！"));
            }
            currModel.Code = model.Code;
            currModel.Icon = model.Icon;
            currModel.LanguageTypeID = model.LanguageTypeID;
            currModel.LastUpdateTime = DateTime.Now;
            currModel.LastUpdateUserID = _currUser.ID;
            currModel.Name = model.Name;
            currModel.ParentId = model.ParentId;
            currModel.Remarks = model.Remarks;
            currModel.SerialNumber = model.SerialNumber;
            currModel.Type = model.Type;
            currModel.Url = model.Url;

            bool IsSucceed = _UserMenuAppService.Edit(currModel);
            if (IsSucceed)
            {
                return Json(new OperateMessage<string>("/UserMenu/Index"));
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
            bool IsSucceed = _UserMenuAppService.DeleteBatch(delIds);
            return Json(new { ret = IsSucceed ? "0000" : "-999", msg = IsSucceed ? "删除成功！" : "删除失败，请先删除外键关联记录！" });
        }
        #endregion
    }
}