using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Vic.Core.Application;
using Vic.Core.Domain.IRepositories;
using Vic.Core.Application.Services;
using Vic.Core.Data.Repositories;
using Vic.Core.Data;
using Vic.Core.Application.IServices;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Razor;
using NLog.Extensions.Logging;
using System.Text;

namespace Ruiec.Home
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

            builder.AddEnvironmentVariables();
            //初始化映射关系
            EntityDtoMapper.Initialize();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //获取数据库连接字符串
            var sqlConnectionString = Configuration.GetConnectionString("Default");

            //添加数据上下文
            services.AddDbContext<TradeDbContext>(options => options.UseSqlServer(sqlConnectionString));
            //依赖注入
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMenuAppService, MenuAppService>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDepartmentAppService, DepartmentAppService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleAppService, RoleAppService>();
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

            //Session服务
            services.AddDistributedRedisCache(option =>
            {
                //redis 数据库连接字符串
                option.Configuration = Configuration.GetConnectionString("RedisConnection");

                //redis 实例名
                option.InstanceName = "master";
            });
            services.AddSession();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

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

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();//添加NLog
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //初始化数据
            SeedData.Initialize(app.ApplicationServices);
        }
    }
}
