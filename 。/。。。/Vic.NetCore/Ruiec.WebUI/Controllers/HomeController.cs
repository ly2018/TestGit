using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Vic.Core.Utils;
using Vic.Core.Application.IServices;
using Vic.Core.Utils.Models;
using Vic.Core.Application.Dtos;
using Vic.Core.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Vic.Core.Domain.Entities;
using Vic.Core.Utils.Enum;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace Ruiec.WebUI.Controllers
{
    [Authorize(AuthenticationSchemes = ConstDefine.AuthenticationScheme)]
    public class HomeController : BaseController
    {
        private readonly IStringLocalizer<HomeController> _localizer;

        private readonly ISiteSettingAppService _siteSettingAppService;
        private readonly ICustomerUserAppService _customerUserAppService;
        private readonly IArticleAppService _articleAppService;
        private readonly IArticleTypeAppService _articleTypeAppService;
        private readonly ILanguageTypeAppService _languageTypeAppService;
        private readonly IPlateSettingAppService _plateSettingAppService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IStringLocalizer<HomeController> localizer, IUnitOfWork unitOfWork, IServiceProvider serviceProvider, IPlateSettingAppService plateSettingAppService, ILanguageTypeAppService languageTypeAppService, ISiteSettingAppService siteSettingAppService, ICustomerUserAppService customerUserAppService, IArticleAppService articleAppService, IArticleTypeAppService articleTypeAppService)
        {
            _customerUserAppService = customerUserAppService;
            _siteSettingAppService = siteSettingAppService;
            _articleAppService = articleAppService;
            _articleTypeAppService = articleTypeAppService;
            _languageTypeAppService = languageTypeAppService;
            _plateSettingAppService = plateSettingAppService;
            _serviceProvider = serviceProvider;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        public IActionResult Test()
        {
            ModelState.Clear();
            ViewBag.Text = _localizer["Hello world!"];
            ViewBag.Te = _localizer.Plural(8, "There is one item.", "There are {0} items.");
            return View();
        }
        public IActionResult Index()
        {
            //try
            //{
            //    _unitOfWork.BeginTransaction();
            //    var setting = _unitOfWork.Get<PlateSetting>(a => a.Key == "test");
            //    _unitOfWork.RegisterDeleted<PlateSetting>(setting.Result);

            //    var language = _unitOfWork.Get<LanguageType>(a => a.Code == "zh-CN");

            //    _unitOfWork.RegisterDeleted<LanguageType>(language.Result);

            //    _unitOfWork.CommitAsync();

            //}
            //catch (Exception ex)
            //{
            //    _unitOfWork.Rollback();
            //}
            //using (var context = new TradeDbContext(_serviceProvider.GetRequiredService<DbContextOptions<TradeDbContext>>()))
            //{

            //    using (var tran = context.Database.BeginTransaction())
            //    {
            //        try
            //        {
            //            context.LanguageType.Add(new LanguageType()
            //            {
            //                Code = "zh-CN",
            //                Images = "zhcn.jpg",
            //                Title = "中文",
            //                CreateTime = DateTime.Now,
            //                LastUpdateTime = DateTime.Now,
            //                LastUpdateUserID = Vic.Core.Utils.ConstDefine.SuperAdminID,
            //                CreatorID = Vic.Core.Utils.ConstDefine.SuperAdminID,
            //            });
            //            context.PlateSetting.AddRange(new PlateSetting()
            //            {
            //                Key = "test",
            //                Tag = "test",
            //                Value = "test",
            //                CreateTime = DateTime.Now,
            //                LastUpdateTime = DateTime.Now,
            //                LastUpdateUserID = Vic.Core.Utils.ConstDefine.SuperAdminID,
            //                CreatorID = Vic.Core.Utils.ConstDefine.SuperAdminID,
            //            }, new PlateSetting()
            //            {
            //                Key = "test",
            //                Tag = "test",
            //                Value = "test",
            //                CreateTime = DateTime.Now,
            //                LastUpdateTime = DateTime.Now,
            //                LastUpdateUserID = Vic.Core.Utils.ConstDefine.SuperAdminID,
            //                CreatorID = Vic.Core.Utils.ConstDefine.SuperAdminID,
            //            });
            //            context.SaveChanges();
            //            tran.Commit();
            //        }
            //        catch (Exception ex)
            //        {
            //            tran.Rollback();
            //        }
            //    }
            //}
            //try
            //{
            //    _unitOfWork.BeginTransaction();
            //    var setting = new PlateSetting()
            //    {
            //        Key = "test",
            //        Tag = "test",
            //        Value = "test",
            //        CreateTime = DateTime.Now,
            //        LastUpdateTime = DateTime.Now,
            //        LastUpdateUserID = Vic.Core.Utils.ConstDefine.SuperAdminID,
            //        CreatorID = Vic.Core.Utils.ConstDefine.SuperAdminID,
            //    };
            //    _unitOfWork.RegisterNew<PlateSetting>(setting);
            //    var language = new LanguageType()
            //    {
            //        Code = "zh-CN",
            //        Images = "zhcn.jpg",
            //        Title = "中文",
            //        CreateTime = DateTime.Now,
            //        LastUpdateTime = DateTime.Now,
            //        LastUpdateUserID = Vic.Core.Utils.ConstDefine.SuperAdminID,
            //        CreatorID = Vic.Core.Utils.ConstDefine.SuperAdminID,
            //    };
            //    _unitOfWork.RegisterNew<LanguageType>(language);
            //    _unitOfWork.CommitAsync();

            //}
            //catch (Exception ex)
            //{
            //    _unitOfWork.Rollback();
            //}

            var data = _plateSettingAppService.List();
            data = _plateSettingAppService.ListByReadOnlyDB();

            this.ModelState.Clear();
            Home_ViewModel VM = new Home_ViewModel(DateTime.Now.Year);
            return View("About", VM);
        }

        public IActionResult Home()
        {
            this.ModelState.Clear();
            var currDto = _siteSettingAppService.Get(a => a.IsAdmin);
            if (currDto != null)
            {
                currDto.QrCode = Common.GetImgFullUrl(currDto.QrCode, Vic.Core.Utils.Enum.AM_Enum.ImgPathEnum.SysImg, true);
                currDto.LogoIco = Common.GetImgFullUrl(currDto.LogoIco, Vic.Core.Utils.Enum.AM_Enum.ImgPathEnum.SysImg, false);
                currDto.Logo = Common.GetImgFullUrl(currDto.Logo, Vic.Core.Utils.Enum.AM_Enum.ImgPathEnum.SysImg, false);
            }
            return View("Index", currDto);
        }
        //初始化导航
        [HttpGet]
        [AllowAnonymous]
        public JsonResult InitData(int j = 1)
        {
            Vic.Core.Utils.Spider.DotNetDocHelper doc = new Vic.Core.Utils.Spider.DotNetDocHelper();
            var data = doc.InitDotNetDocHelper(j);
            if (data.Any())
            {
                var langID = _languageTypeAppService.List().FirstOrDefault().ID;
                //_languageTypeAppService.Create(new LanguageTypeDto()
                //{
                //    ID = langID,
                //    Code = "zh-CN",
                //    Images = "https://wx1.sinaimg.cn/large/006WIu6sgy1fjslc61hrtj30rs0fmtbj.jpg",
                //    Title = "中文",
                //    CreateTime = DateTime.Now,
                //    LastUpdateTime = DateTime.Now,
                //    LastUpdateUserID = Vic.Core.Utils.ConstDefine.SuperAdminID,
                //    CreatorID = Vic.Core.Utils.ConstDefine.SuperAdminID
                //});
                var articleType = _articleTypeAppService.Get(a => a.SerialNumber == j);
                //var exists = _articleAppService.Get(a => a.ArticleTypeID == articleType.ID);
                //if (exists != null)
                //{
                //    return Json("exists");
                //}
                foreach (var item in data)
                {
                    if (string.IsNullOrEmpty(item.content))
                    {
                        continue;
                    }
                    //_articleTypeAppService.Create(new ArticleTypeDto()
                    //{
                    //    ID = Guid.NewGuid(),
                    //    SerialNumber = i,
                    //    Title = item.title,
                    //    Url = item.cover,
                    //    LanguageTypeID = langID,
                    //    LanguageTypeName = "中文",
                    //    Images = "https://wx1.sinaimg.cn/large/006WIu6sgy1fjslc61hrtj30rs0fmtbj.jpg",
                    //    CreateTime = DateTime.Now,
                    //    LastUpdateTime = DateTime.Now,
                    //    LastUpdateUserID = Vic.Core.Utils.ConstDefine.SuperAdminID,
                    //    CreatorID = Vic.Core.Utils.ConstDefine.SuperAdminID
                    //});
                    int count = _articleAppService.Count(a => a.Title == item.title);
                    if (count > 0) continue;
                    _articleAppService.Create(new ArticleDto()
                    {
                        //ID = Guid.NewGuid(),
                        Title = item.title,
                        ArticleTypeID = articleType.ID,
                        ArticleTypeName = articleType.Title,
                        Author = "gaoby.cn",
                        BrowseNum = 101,
                        Content = item.content,
                        Description = item.content.Length > 500 ? item.content.Substring(0, 500) : item.content,
                        PublishDate = DateTime.Now,
                        Remark = "",
                        SeoTitle = item.title,
                        Source = item.cover,
                        LanguageTypeID = langID,
                        LanguageTypeName = "中文",
                        Images = "https://wx1.sinaimg.cn/large/006WIu6sgy1fjslc61hrtj30rs0fmtbj.jpg",
                        CreateTime = DateTime.Now,
                        LastUpdateTime = DateTime.Now,
                        LastUpdateUserID = Vic.Core.Utils.ConstDefine.SuperAdminID,
                        CreatorID = Vic.Core.Utils.ConstDefine.SuperAdminID
                    });
                }
            }
            return Json("ok");
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult PublishData()
        {
            var ids = _plateSettingAppService.QueryProperty(a => a.Tag == "", a => a.ID).ToArray();
            var data = _articleAppService.QueryList(a => !ids.Contains(a.ID));
            int cpunt = 0;
            foreach (var item in data)
            {
                if (string.IsNullOrEmpty(item.Content))
                {
                    continue;
                }
                int count = _plateSettingAppService.Count(a => a.ID == item.ID);
                if (count > 0)
                {
                    continue;
                }
                var ret = Vic.Core.Utils.Spider.WeiBoHelper.PublishArticle(new Vic.Core.Utils.Spider.ReqModel()
                {
                    title = "【AspNetCore 2.0】" + item.Title,// + " @AspNetCore @告白驿站",
                    summary = item.Description.Length > 200 ? item.Description.Substring(1, 200) : item.Description,
                    content = "【AspNetCore 2.0 】" + item.Content,
                });
                if (ret != null && ret.data != null && !string.IsNullOrEmpty(ret.data.object_id))
                {
                    _plateSettingAppService.Create(new PlateSettingDto()
                    {
                        ID = item.ID,
                        Key = item.ID.ToString(),
                        Value = item.ID.ToString(),
                        Tag = "",
                        CreateTime = DateTime.Now,
                        LastUpdateTime = DateTime.Now,
                        LastUpdateUserID = Vic.Core.Utils.ConstDefine.SuperAdminID,
                        CreatorID = Vic.Core.Utils.ConstDefine.SuperAdminID
                    });
                    cpunt++;
                }
                System.Threading.Thread.Sleep(1000 * 60 * 5);
            }
            return Json("ok_" + cpunt);
        }

        #region 获取用户统计数据
        /// <summary>
        /// 获取用户统计数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetUserData(int currYear)
        {
            if (currYear <= 0)
            {
                currYear = DateTime.Now.Year;
            }
            int TotalCount = _customerUserAppService.Count(a => a.ID != Guid.Empty);
            var parameterExpression = System.Linq.Expressions.Expression.Parameter(typeof(CustomerUser));
            var filter = (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Constant(true);
            filter = filter.GotoAndAlso(parameterExpression.GotoGreaterThanByDateTime("CreateTime", DateTime.Parse(string.Format("{0}-01-01", currYear))));
            filter = filter.GotoAndAlso(parameterExpression.GotoLessThanByDateTime("CreateTime", DateTime.Parse(string.Format("{0}-01-01", currYear + 1))));
            var queryLambdaFilter = System.Linq.Expressions.Expression.Lambda<Func<CustomerUser, bool>>(filter, parameterExpression);
            List<CustomerUserDto> UserList = _customerUserAppService.QueryList(queryLambdaFilter);
            Root data = new Root();
            DateTime date = DateTime.Now.Date;
            DateTime dateNext = date.AddDays(1);
            int dayRegister = _customerUserAppService.Count(a => a.ID != Guid.Empty && a.CreateTime >= date && a.CreateTime < dateNext);
            data.title = new Title()
            {
                text = currYear + "年注册用户统计(总注册数：" + TotalCount + "，本年度注册数：" + UserList.Count + "，当日注册人数：" + dayRegister + ")",
                subtext = " 月 / 人",
            };
            data.tooltip = new Tooltip() { trigger = "axis" };
            List<string> legendList = new List<string>();
            legendList.Add("注册用户");
            data.legend = new Legend()
            {
                data = legendList
            };
            data.toolbox = new Toolbox()
            {
                show = true,
                feature = new Feature()
                {
                    dataView = new DataView()
                    {
                        readOnly = false,
                        show = true,
                    },
                    saveAsImage = new SaveAsImage()
                    {
                        show = true,
                    },
                    magicType = new MagicType()
                    {
                        show = true,
                        type = new List<string>() { "line", "bar" }
                    },
                    restore = new Restore()
                    {
                        show = true
                    },
                    mark = new Mark()
                    {
                        show = true
                    }
                }
            };
            data.calculable = true;
            data.xAxis = new List<XAxis>() {
                new XAxis()
        {
            data = new List<string>() { "1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月" },
                type = "category"
                }
    };
            data.yAxis = new List<YAxis>() {
                new YAxis()
    {
        type = "value"
                }
};
            List<string> dataList = new List<string>();
            string sum = "0";
            for (int i = 1; i < 13; i++)
            {
                sum = UserList.Where(a => a.CreateTime.Month == i).Count().ToString();
                dataList.Add(sum);
            }
            data.series = new List<Series>() {
                new Series()
{
    name = "注册用户",
                    type = "bar",
                    data = dataList,
                    markLine = new MarkLine()
                    {
                        data = new List<AverageData>() {
                            new AverageData() {
                                name="最大值",
                                type="max"
                            },
                        new AverageData() {
                            name ="最小值",
                            type="min"
                        } }
                    },
                    markPoint = new MarkPoint()
                    {
                        data = new List<MaxMinData>() {
                            new MaxMinData() {
                                name="平均值",
                                type="average"
                            }
                        }
                    }
                }
            };
            return Json(data);
        }
        #endregion
    }
}
