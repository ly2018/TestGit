using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vic.Core.Application.IServices;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Vic.Core.Data;
using Microsoft.Extensions.Logging;
using NLog;

namespace Ruiec.Home.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IServiceProvider _serviceProvider;
        private readonly IDepartmentAppService _departmentAppService;
        private readonly IUserAppService _service;
        private readonly IArticleAppService _articleService;
        private readonly ILogger<HomeController> logger;

        public HomeController(IArticleAppService articleAppService, ILogger<HomeController> Iogger, IUserAppService service, IStringLocalizer<HomeController> localizer, IServiceProvider serviceProvider, IDepartmentAppService departmentAppService)
        {
            logger = Iogger;
            _service = service;
            _localizer = localizer;
            _serviceProvider = serviceProvider;
            _departmentAppService = departmentAppService;
            _articleService = articleAppService;
        }
        public IActionResult Index()
        {
            try
            {
                bool isOk = _articleService.Create(new Vic.Core.Application.Dtos.ArticleDto()
                {
                    ArticleTypeID = Guid.NewGuid(),
                    Author = "测试",
                    BrowseNum = 0,
                    Content = "asdfsa",
                    CreateTime = DateTime.Now,
                    CreatorID = Vic.Core.Utils.ConstDefine.SuperAdminID,
                    Description = "asdas",
                    ID = Guid.NewGuid(),
                    Images = "测试.jpg",
                    LanguageTypeID = Guid.NewGuid(),
                    PublishDate = DateTime.Now.AddDays(-7),
                    Remark = "测试",
                    SeoTitle = "测试",
                    Source = "测试",
                    Title = "测试",
                });
                logger.LogDebug("Logger 测试1");
                logger.LogError("Logger 测试2");
                logger.LogInformation("Logger 测试3");
                logger.LogWarning("Logger 测试4");
                var data = _departmentAppService.ListByReadOnlyDB();
                HttpContext.Session.SetString("testsession", "redis session");
                ViewBag.Btn_Submit = _localizer["Btn_Submit"];
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(_service.Get(new Guid("C660FCDB-B3C1-49DA-6C6B-08D4A40472B8")));
        }

        public IActionResult About()
        {
            var s = HttpContext.Session.GetString("testsession");
            //事务操作
            using (var context = new TradeDbContext(_serviceProvider.GetRequiredService<DbContextOptions<TradeDbContext>>()))
            {
                Guid departmentId = Guid.NewGuid();
                //增加一个部门
                context.Departments.Add(
                   new Vic.Core.Domain.Entities.Department
                   {
                       ID = departmentId,
                       Code = "BD",
                       Name = "商务部",
                       ParentId = Guid.Empty
                   }
                );

                context.Roles.Add(new Vic.Core.Domain.Entities.Role()
                {
                    ID = Guid.NewGuid(),
                    Code = "Aduit",
                    CreateTime = DateTime.Now,
                    Name = "认证员",
                    Remarks = "认证员"
                });
                //增加一个超级管理员用户
                context.Users.Add(
                     new Vic.Core.Domain.Entities.User
                     {
                         UserName = "adminx",
                         Password = "123456", //暂不进行加密
                         Name = "管理员",
                         DepartmentId = departmentId
                     }
                );

                context.CustomerUser.Add(new Vic.Core.Domain.Entities.CustomerUser()
                {
                    Mobile = "13430932507",
                    AuthenticationVerify = 1,
                    VerifyResult = 0,
                    NickName = "VIC",
                    RealName = "VIC LI",
                    Type = 1,
                    Status = 1,
                    PassWord = "123456",
                    TPassWord = "123456",
                    CreatorID = Vic.Core.Utils.ConstDefine.SuperAdminID
                });

                context.SaveChanges();
            }
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
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
    }
}
