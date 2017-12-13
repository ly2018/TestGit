using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Vic.Core.Application.Dtos;
using Vic.Core.Domain.Entities;

namespace Vic.Core.Application
{
    // <summary>
    /// Enity与Dto映射
    /// </summary>
    public class EntityDtoMapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Menu, MenuDto>();
                cfg.CreateMap<MenuDto, Menu>();
                cfg.CreateMap<Department, DepartmentDto>();
                cfg.CreateMap<DepartmentDto, Department>();
                cfg.CreateMap<RoleDto, Role>();
                cfg.CreateMap<Role, RoleDto>();
                cfg.CreateMap<RoleMenuDto, RoleMenu>();
                cfg.CreateMap<RoleMenu, RoleMenuDto>();
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserRoleDto, UserRole>();
                cfg.CreateMap<UserRole, UserRoleDto>();

                cfg.CreateMap<ArticleDto, Article>();
                cfg.CreateMap<Article, ArticleDto>();

                cfg.CreateMap<ArticleTypeDto, ArticleType>();
                cfg.CreateMap<ArticleType, ArticleTypeDto>();

                cfg.CreateMap<BannerDto, Banner>();
                cfg.CreateMap<Banner, BannerDto>();

                cfg.CreateMap<LanguageTypeDto, LanguageType>();
                cfg.CreateMap<LanguageType, LanguageTypeDto>();

                cfg.CreateMap<MailConfigDto, MailConfig>();
                cfg.CreateMap<MailConfig, MailConfigDto>();

                cfg.CreateMap<MailTemplateDto, MailTemplate>();
                cfg.CreateMap<MailTemplate, MailTemplateDto>();

                cfg.CreateMap<PlateSettingDto, PlateSetting>();
                cfg.CreateMap<PlateSetting, PlateSettingDto>();

                cfg.CreateMap<PushServiceDto, PushService>();
                cfg.CreateMap<PushService, PushServiceDto>();

                cfg.CreateMap<ShortMsgConfigDto, ShortMsgConfig>();
                cfg.CreateMap<ShortMsgConfig, ShortMsgConfigDto>();

                cfg.CreateMap<ShortMsgTemplateDto, ShortMsgTemplate>();
                cfg.CreateMap<ShortMsgTemplate, ShortMsgTemplateDto>();

                cfg.CreateMap<SysLogDto, SysLog>();
                cfg.CreateMap<SysLog, SysLogDto>();

                cfg.CreateMap<UserMenuDto, UserMenu>();
                cfg.CreateMap<UserMenu, UserMenuDto>();

                cfg.CreateMap<VerifyCodeDto, VerifyCode>();
                cfg.CreateMap<VerifyCode, VerifyCodeDto>();

                cfg.CreateMap<CustomerUserDto, CustomerUser>();
                cfg.CreateMap<CustomerUser, CustomerUserDto>();

                cfg.CreateMap<SiteSettingDto, SiteSetting>();
                cfg.CreateMap<SiteSetting, SiteSettingDto>();

            });
        }
    }
}