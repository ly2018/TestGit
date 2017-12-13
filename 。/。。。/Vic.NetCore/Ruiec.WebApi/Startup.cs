using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Vic.Core.Data.Repositories;
using Vic.Core.Application.Services;
using Vic.Core.Application.IServices;
using Vic.Core.Data;
using Microsoft.EntityFrameworkCore;
using Vic.Core.Application;
using Microsoft.Net.Http.Headers;
using NLog.Extensions.Logging;
using System.Text;
using Microsoft.AspNetCore.Http;
using Vic.Core.Domain.IRepositories;
using NLog.Config;
using Vic.Core.Utils.WebApi;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace Ruiec.WebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            #region 初始化映射关系
            //初始化映射关系
            EntityDtoMapper.Initialize();
            #endregion
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            #region 数据访问层注入
            // Add framework services.
            //获取数据库连接字符串
            var sqlConnectionString = Configuration.GetConnectionString("Default");
            var sqlReadOnlyConnectionString = Configuration.GetConnectionString("ReadOnly");

            //添加数据上下文
            services.AddDbContext<TradeDbContext>(options => options.UseSqlServer(sqlConnectionString));
            services.AddDbContext<ReadOnlyTradeDbContext>(options => options.UseSqlServer(sqlReadOnlyConnectionString));

            //依赖注入
            #region 依赖注入
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMenuAppService, MenuAppService>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDepartmentAppService, DepartmentAppService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleAppService, RoleAppService>();


            services.AddScoped<ISiteSettingRepository, SiteSettingRepository>();
            services.AddScoped<ISiteSettingAppService, SiteSettingAppService>();


            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IArticleAppService, ArticleAppService>();

            services.AddScoped<IArticleTypeRepository, ArticleTypeRepository>();
            services.AddScoped<IArticleTypeAppService, ArticleTypeAppService>();

            services.AddScoped<IBannerRepository, BannerRepository>();
            services.AddScoped<IBannerAppService, BannerAppService>();

            services.AddScoped<ILanguageTypeRepository, LanguageTypeRepository>();
            services.AddScoped<ILanguageTypeAppService, LanguageTypeAppService>();

            services.AddScoped<IMailConfigRepository, MailConfigRepository>();
            services.AddScoped<IMailConfigAppService, MailConfigAppService>();

            services.AddScoped<IMailTemplateRepository, MailTemplateRepository>();
            services.AddScoped<IMailTemplateAppService, MailTemplateAppService>();

            services.AddScoped<IPlateSettingRepository, PlateSettingRepository>();
            services.AddScoped<IPlateSettingAppService, PlateSettingAppService>();

            services.AddScoped<IPushServiceRepository, PushServiceRepository>();
            services.AddScoped<IPushServiceAppService, PushServiceAppService>();

            services.AddScoped<IShortMsgConfigRepository, ShortMsgConfigRepository>();
            services.AddScoped<IShortMsgConfigAppService, ShortMsgConfigAppService>();

            services.AddScoped<IShortMsgTemplateRepository, ShortMsgTemplateRepository>();
            services.AddScoped<IShortMsgTemplateAppService, ShortMsgTemplateAppService>();

            services.AddScoped<ISysLogRepository, SysLogRepository>();
            services.AddScoped<ISysLogAppService, SysLogAppService>();

            services.AddScoped<IUserMenuRepository, UserMenuRepository>();
            services.AddScoped<IUserMenuAppService, UserMenuAppService>();

            services.AddScoped<IVerifyCodeRepository, VerifyCodeRepository>();
            services.AddScoped<IVerifyCodeAppService, VerifyCodeAppService>();

            services.AddScoped<ICustomerUserRepository, CustomerUserRepository>();
            services.AddScoped<ICustomerUserAppService, CustomerUserAppService>();
            #endregion
            #endregion

            #region 多语言注入
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            // Add framework services.
            services.AddMvc()
                // Add support for finding localized views, based on file name suffix, e.g. Index.fr.cshtml
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                // Add support for localizing strings in data annotations (e.g. validation messages) via the
                // IStringLocalizer abstractions.
                .AddDataAnnotationsLocalization();

            // Configure supported cultures and localization options
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("zh-CN"),
                    new CultureInfo("zh-TW")
                };

                // State what the default culture for your application is. This will be used if no specific culture
                // can be determined for a given request.
                options.DefaultRequestCulture = new RequestCulture(culture: "zh-CN", uiCulture: "zh-CN");

                // You must explicitly state which cultures your application supports.
                // These are the cultures the app supports for formatting numbers, dates, etc.
                options.SupportedCultures = supportedCultures;

                // These are the cultures the app supports for UI strings, i.e. we have localized resources for.
                options.SupportedUICultures = supportedCultures;

                // You can change which providers are configured to determine the culture for requests, or even add a custom
                // provider with your own logic. The providers will be asked in order to provide a culture for each request,
                // and the first to provide a non-null result that is in the configured supported cultures list will be used.
                // By default, the following built-in providers are configured:
                // - QueryStringRequestCultureProvider, sets culture via "culture" and "ui-culture" query string values, useful for testing
                // - CookieRequestCultureProvider, sets culture via "ASPNET_CULTURE" cookie
                // - AcceptLanguageHeaderRequestCultureProvider, sets culture via the "Accept-Language" request header
                //options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
                //{
                //  // My custom request culture logic
                //  return new ProviderCultureResult("en");
                //}));
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            #region 添加NLog
            loggerFactory.AddNLog();//添加NLog,bin\Debug\netcoreapp2.0 文件夹加上nlog.config文件
            #endregion

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            #region 添加自定义中间件
            app.UseApiLogger();
            #endregion

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller=Home}/{action=Home}/{id?}");
            });
        }
    }
}
