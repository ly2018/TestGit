using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

using Vic.Core.Application.Dtos;
using Vic.Core.Application.IServices;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Ruiec.Admin.Controllers
{
    public abstract class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            byte[] result;
            filterContext.HttpContext.Session.TryGetValue("CurrentUser", out result);
            if (result == null)
            {
                filterContext.Result = new RedirectResult("/Login/Index");
                return;
            }
            base.OnActionExecuting(filterContext);
        }

        public UserDto _currUser;

        /// <summary>
        /// 获取服务端验证的第一条错误信息
        /// </summary>
        /// <returns></returns>
        public string GetModelStateError()
        {
            foreach (var item in ModelState.Values)
            {
                if (item.Errors.Count > 0)
                {
                    return item.Errors[0].ErrorMessage;
                }
            }
            return "";
        }
        
        /// <summary>
        /// 分页代码(支持参数化分页)
        /// </summary>
        /// <param name="Total">总页数</param>
        /// <param name="Curpage">当前页</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="context"></param>
        /// <returns></returns>
        public  string PageHtml(int Total, int Curpage, int PageSize, HttpContext context)
        {
            int pageCount = (int)Math.Ceiling((double)Total / PageSize);
            string[] arr = context.Request.Query.Keys.ToArray();
            string PathAndQuery = context.Request.Path.Value+ context.Request.QueryString.Value;
            if (PathAndQuery.Contains("?"))
            {
                PathAndQuery = PathAndQuery.Substring(0, PathAndQuery.LastIndexOf("?") + 1);
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] != "CurPage")
                    {
                        PathAndQuery += arr[i] + "=" + context.Request.Query[arr[i]] + "&";
                    }
                }
            }
            else
            {
                PathAndQuery += "?";
            }
            string str = string.Empty;
            StringBuilder sb = new StringBuilder();
            if (Curpage == 1)
            {
                sb.Append("<span class=\"noPage\">首页</span>  ");
                sb.Append("<span class=\"noPage\">上一页</span>  ");
            }
            else
            {
                sb.Append("<a href=\"" + PathAndQuery + "CurPage=1\" title=\"转至第1页\" class=\"a1\">首页</a> ");
                sb.AppendFormat("<a href=\"" + PathAndQuery + "CurPage={0}\" title=\"转至第{1}页\" class=\"a1\">上一页</a> ", Curpage - 1, Curpage - 1);
            }

            if (pageCount <= 9)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    if (i == Curpage)
                    {
                        sb.AppendFormat("<span class=\"curPage\">{0}</span> ", i);
                    }
                    else
                    {
                        sb.AppendFormat("<a href=\"" + PathAndQuery + "CurPage={0}\" title=\"转至第{1}页\">{1}</a> ", i, i);
                    }
                }
            }
            else
            {
                if (Curpage <= 5)
                {
                    for (int i = 1; i <= 9; i++)
                    {
                        if (i == Curpage)
                        {
                            sb.AppendFormat("<span class=\"curPage\">{0}</span> ", i);
                        }
                        else
                        {
                            sb.AppendFormat("<a href=\"" + PathAndQuery + "CurPage={0}\" title=\"转至第{1}页\">{1}</a> ", i, i);
                        }
                    }

                }
                else if (Curpage > (pageCount - 5))
                {
                    for (int i = 1; i <= 9; i++)
                    {
                        if ((pageCount - 9 + i) == Curpage)
                        {
                            sb.AppendFormat("<span class=\"curPage\">{0}</span> ", (pageCount - 9 + i));
                        }
                        else
                        {
                            sb.AppendFormat("<a href=\"" + PathAndQuery + "CurPage={0}\" title=\"转至第{1}页\">{1}</a> ", (pageCount - 9 + i), (pageCount - 9 + i));
                        }
                    }
                }
                else
                {
                    for (int i = 1; i <= 9; i++)
                    {
                        if ((Curpage - 5 + i) == Curpage)
                        {
                            sb.AppendFormat("<span class=\"curPage\">{0}</span> ", (Curpage - 5 + i));
                        }
                        else
                        {
                            sb.AppendFormat("<a href=\"" + PathAndQuery + "CurPage={0}\" title=\"转至第{1}页\">{1}</a> ", (Curpage - 5 + i), (Curpage - 5 + i));
                        }
                    }

                }
            }
            if (Curpage == pageCount)
            {
                sb.Append("<span class=\"noPage\">下一页</span>  ");
                sb.Append("<span class=\"noPage\">尾页</span>   ");
            }
            else
            {
                sb.AppendFormat("<a href=\"" + PathAndQuery + "CurPage={0}\" title=\"转至第{1}页\" class=\"a1\">下一页</a>  ", Curpage + 1, Curpage + 1);
                sb.AppendFormat("<a href=\"" + PathAndQuery + "CurPage={0}\" title=\"转至第{1}页\" class=\"a1\">尾页</a>  ", pageCount, pageCount);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 分页代码(支持多语言参数化分页)
        /// </summary>
        /// <param name="Total">总页数</param>
        /// <param name="Curpage">当前页</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="CurrentCulture">语言类型zh-CN或en</param>
        /// <returns></returns>
        public string PageHtml(int Total, int Curpage, int PageSize, HttpContext context, string CurrentCulture)
        {
            int pageCount = (int)Math.Ceiling((double)Total / PageSize);
            string[] arr = context.Request.Query.Keys.ToArray();
            string PathAndQuery = context.Request.Path.Value + context.Request.QueryString.Value;
            if (PathAndQuery.Contains("?"))
            {
                PathAndQuery = PathAndQuery.Substring(0, PathAndQuery.LastIndexOf("?") + 1);
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] != "CurPage")
                    {
                        PathAndQuery += arr[i] + "=" + context.Request.Query[arr[i]] + "&";
                    }
                }
            }
            else
            {
                PathAndQuery += "?";
            }
            string str = string.Empty;
            StringBuilder sb = new StringBuilder();
            if (Curpage == 1)
            {
                if (CurrentCulture.Equals("zh-CN"))
                {
                    sb.Append("<span class=\"noPage\">首页</span>  ");
                    sb.Append("<span class=\"noPage\">上一页</span>  ");
                }
                if (CurrentCulture.Equals("en"))
                {
                    sb.Append("<span class=\"noPage\">HomePage</span>  ");
                    sb.Append("<span class=\"noPage\">PageUp</span>  ");
                }
            }
            else
            {
                if (CurrentCulture.Equals("zh-CN"))
                {
                    sb.Append("<a href=\"" + PathAndQuery + "CurPage=1\" title=\"转至第1页\" class=\"a1\">首页</a> ");
                    sb.AppendFormat("<a href=\"" + PathAndQuery + "CurPage={0}\" title=\"转至第{1}页\" class=\"a1\">上一页</a> ", Curpage - 1, Curpage - 1);
                }
                if (CurrentCulture.Equals("en"))
                {
                    sb.Append("<a href=\"" + PathAndQuery + "CurPage=1\" title=\"jumpfirstpage\" class=\"a1\">HomePage</a> ");
                    sb.AppendFormat("<a href=\"" + PathAndQuery + "CurPage={0}\" title=\"jump{1}page\" class=\"a1\">PageUp</a> ", Curpage - 1, Curpage - 1);
                }

            }

            if (pageCount <= 9)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    if (i == Curpage)
                    {
                        sb.AppendFormat("<span class=\"curPage\">{0}</span> ", i);
                    }
                    else
                    {
                        if (CurrentCulture.Equals("zh-CN"))
                        {
                            sb.AppendFormat("<a href=\"" + PathAndQuery + "CurPage={0}\" title=\"转至第{1}页\">{1}</a> ", i, i);
                        }
                        if (CurrentCulture.Equals("en"))
                        {
                            sb.AppendFormat("<a href=\"" + PathAndQuery + "CurPage={0}\" title=\"jump{1}page\">{1}</a> ", i, i);
                        }

                    }
                }
            }
            else
            {
                if (Curpage <= 5)
                {
                    for (int i = 1; i <= 9; i++)
                    {
                        if (i == Curpage)
                        {
                            sb.AppendFormat("<span class=\"curPage\">{0}</span> ", i);
                        }
                        else
                        {
                            if (CurrentCulture.Equals("zh-CN"))
                            {
                                sb.AppendFormat("<a href=\"" + PathAndQuery + "CurPage={0}\" title=\"转至第{1}页\">{1}</a> ", i, i);
                            }
                            if (CurrentCulture.Equals("en"))
                            {
                                sb.AppendFormat("<a href=\"" + PathAndQuery + "CurPage={0}\" title=\"jump{1}page\">{1}</a> ", i, i);
                            }
                        }
                    }

                }
                else if (Curpage > (pageCount - 5))
                {
                    for (int i = 1; i <= 9; i++)
                    {
                        if ((pageCount - 9 + i) == Curpage)
                        {
                            sb.AppendFormat("<span class=\"curPage\">{0}</span> ", (pageCount - 9 + i));
                        }
                        else
                        {
                            if (CurrentCulture.Equals("zh-CN"))
                            {
                                sb.AppendFormat("<a href=\"" + PathAndQuery + "CurPage={0}\" title=\"转至第{1}页\">{1}</a> ", (pageCount - 9 + i), (pageCount - 9 + i));
                            }
                            if (CurrentCulture.Equals("en"))
                            {
                                sb.AppendFormat("<a href=\"" + PathAndQuery + "CurPage={0}\" title=\"jump{1}page\">{1}</a> ", (pageCount - 9 + i), (pageCount - 9 + i));
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 1; i <= 9; i++)
                    {
                        if ((Curpage - 5 + i) == Curpage)
                        {
                            sb.AppendFormat("<span class=\"curPage\">{0}</span> ", (Curpage - 5 + i));
                        }
                        else
                        {
                            if (CurrentCulture.Equals("zh-CN"))
                            {
                                sb.AppendFormat("<a href=\"" + PathAndQuery + "CurPage={0}\" title=\"转至第{1}页\">{1}</a> ", (Curpage - 5 + i), (Curpage - 5 + i));
                            }
                            if (CurrentCulture.Equals("en"))
                            {
                                sb.AppendFormat("<a href=\"" + PathAndQuery + "CurPage={0}\" title=\"jump{1}page\">{1}</a> ", (Curpage - 5 + i), (Curpage - 5 + i));
                            }

                        }
                    }

                }
            }
            if (Curpage == pageCount)
            {
                if (CurrentCulture.Equals("zh-CN"))
                {
                    sb.Append("<span class=\"noPage\">下一页</span>  ");
                    sb.Append("<span class=\"noPage\">尾页</span>   ");
                }
                if (CurrentCulture.Equals("en"))
                {
                    sb.Append("<span class=\"noPage\">NextPage</span>  ");
                    sb.Append("<span class=\"noPage\">LastPage</span>   ");
                }

            }
            else
            {
                if (CurrentCulture.Equals("zh-CN"))
                {
                    sb.AppendFormat("<a href=\"" + PathAndQuery + "CurPage={0}\" title=\"转至第{1}页\" class=\"a1\">下一页</a>  ", Curpage + 1, Curpage + 1);
                    sb.AppendFormat("<a href=\"" + PathAndQuery + "CurPage={0}\" title=\"转至第{1}页\" class=\"a1\">尾页</a>  ", pageCount, pageCount);
                }
                if (CurrentCulture.Equals("en"))
                {
                    sb.AppendFormat("<a href=\"" + PathAndQuery + "CurPage={0}\" title=\"jump{1}page\" class=\"a1\">nextpage</a>  ", Curpage + 1, Curpage + 1);
                    sb.AppendFormat("<a href=\"" + PathAndQuery + "CurPage={0}\" title=\"jump{1}page\" class=\"a1\">LastPage</a>  ", pageCount, pageCount);
                }

            }
            return sb.ToString();
        }

    }
}
