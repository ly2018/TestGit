using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Vic.Core.Domain.IRepositories;
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
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;

namespace Ruiec.WebUI
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


            #region 框架服务及多语言注入
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            services.AddPortableObjectLocalization(options => options.ResourcesPath = "Localization");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("zh-CN"),
                new CultureInfo("en"),
                new CultureInfo("zh-TW")
            };

                options.DefaultRequestCulture =new RequestCulture("zh-CN");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            #endregion

            //启用授权
            services.AddAuthentication(Vic.Core.Utils.ConstDefine.AuthenticationScheme)// 验证方案名
            .AddCookie(Vic.Core.Utils.ConstDefine.AuthenticationScheme, m =>
            {
                m.LoginPath = new PathString("/Login/UserLogin");
                m.AccessDeniedPath = new PathString("/Login/UserLogin");
                m.LogoutPath = new PathString("/Login/UserLogOutAsync");
                m.Cookie.Path = "/";
                m.ExpireTimeSpan = TimeSpan.FromHours(1); // 过期时间
                m.SlidingExpiration = true;  // 是否在过期时间过半的时候，自动延期
                                             //m.CookieName = "viccore";             // Cookie 名称
                                             //m.CookiePath = "/";             // Cookie 路径
                                             //m.CookieHttpOnly = true;       // 是否允许客户端Js获取。默认True，不允许。
            });
            //Session服务
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();// 添加NLog,  bin\Debug\netcoreapp2.0 文件夹加上nlog.config文件
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 0;// 60 * 60 * 24;
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] =
            "public,max-age=" + durationInSeconds;
                }
            });

            app.UseAuthentication();
            app.UseSession();

            app.UseRequestLocalization();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Home}/{id?}");
            });
            //初始化数据
            SeedData.Initialize(app.ApplicationServices);

        }
    }
}
