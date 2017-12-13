using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vic.Core.Data;
using Vic.Core.Domain.Entities;
using Vic.Core.Utils.Enum;

namespace Vic.Core.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TradeDbContext(serviceProvider.GetRequiredService<DbContextOptions<TradeDbContext>>()))
            {
                if (context.Users.Any())
                {
                    return;   // 已经初始化过数据，直接返回
                }

                #region 欢迎您菜单
                context.Menus.Add(new Menu
                {
                    Name = "首页",
                    Code = "Home",
                    SerialNumber = 0,
                    ParentId = Guid.Empty,
                    Icon = "fa fa-link",
                    Url = "#",
                    Type = (int)AM_Enum.MenuTypeEnum.ParentMenu,
                    IsTopMenu = true,
                    CreatorID = Utils.ConstDefine.SuperAdminID
                });

                Guid parentWelcome = Guid.NewGuid();
                context.Menus.Add(new Menu
                {
                    ID = parentWelcome,
                    Name = "欢迎您{0}",
                    Code = "Welcome",
                    SerialNumber = 1,
                    ParentId = Guid.Empty,
                    Icon = "fa fa-link",
                    Url = "#",
                    Type = (int)AM_Enum.MenuTypeEnum.ParentMenu,
                    IsTopMenu = true,
                    CreatorID = Utils.ConstDefine.SuperAdminID
                });

                context.Menus.AddRange(new Menu()
                {
                    Name = "个人资料",
                    Code = "UserInfo",
                    SerialNumber = 1,
                    ParentId = parentWelcome,
                    Type = (int)AM_Enum.MenuTypeEnum.ChildMenu,
                    IsTopMenu = true,
                    Icon = "fa fa-link",
                    Url = "/User/UserInfo",
                    CreatorID = Utils.ConstDefine.SuperAdminID
                }, new Menu()
                {
                    Name = "修改密码",
                    Code = "ResetPwd",
                    SerialNumber = 2,
                    ParentId = parentWelcome,
                    Type = (int)AM_Enum.MenuTypeEnum.ChildMenu,
                    IsTopMenu = true,
                    Icon = "fa fa-link",
                    Url = "/User/ResetPwd",
                    CreatorID = Utils.ConstDefine.SuperAdminID
                }, new Menu()
                {
                    Name = "注销",
                    Code = "SignOut",
                    SerialNumber = 3,
                    ParentId = parentWelcome,
                    Type = (int)AM_Enum.MenuTypeEnum.ChildMenu,
                    IsTopMenu = true,
                    Icon = "fa fa-link",
                    Url = "/Login/UserLogOutAsync",
                    CreatorID = Utils.ConstDefine.SuperAdminID
                });

                context.Menus.Add(new Menu
                {
                    Name = "菜单管理",
                    Code = "Department",
                    SerialNumber = 3,
                    ParentId = Guid.Empty,
                    Icon = "fa fa-link",
                    Type = (int)AM_Enum.MenuTypeEnum.ParentMenu,
                    IsTopMenu = true,
                    Url = "/Menu/Index",
                    CreatorID = Utils.ConstDefine.SuperAdminID
                });
                #endregion

                #region 角色权限管理

                Guid departmentId = Utils.ConstDefine.SuperDepartmentID;
                //增加一个部门
                context.Departments.Add(
                   new Department
                   {
                       ID = departmentId,
                       Code = "ZB",
                       Name = "集团总部",
                       ParentId = Guid.Empty,
                       ContactNumber = "13430932507",
                       CreatorID = Utils.ConstDefine.SuperAdminID
                   }
                );
                //增加角色
                context.Roles.Add(new Role()
                {
                    ID = Utils.ConstDefine.SuperRoleID,
                    Code = "SuperRole",
                    Name = "超级管理员",
                    Remarks = "超级管理员",
                    CreatorID = Utils.ConstDefine.SuperAdminID
                });

                //增加一个超级管理员用户
                context.Users.Add(
                     new User
                     {
                         ID = Utils.ConstDefine.SuperAdminID,
                         UserName = "am",
                         Password = Core.Utils.SecurityHelper.EncryptDES("coresys"), //进行加密
                         Name = "超级管理员",
                         MobileNumber = "13430932507",
                         DepartmentId = departmentId,
                         CreatorID = Utils.ConstDefine.SuperAdminID
                     }
                );

                //添加用户角色
                context.UserRoles.Add(new UserRole()
                {
                    RoleId = Utils.ConstDefine.SuperRoleID,
                    UserId = Utils.ConstDefine.SuperAdminID
                });


                Guid roleMenuID = Guid.NewGuid();
                context.Menus.Add(new Menu
                {
                    ID = roleMenuID,
                    Name = "角色权限管理",
                    Code = "Department",
                    SerialNumber = 2,
                    ParentId = Guid.Empty,
                    Icon = "fa fa-link",
                    Type = (int)AM_Enum.MenuTypeEnum.ParentMenu,
                    IsTopMenu = true,
                    Url = "/Menu/Index",
                    CreatorID = Utils.ConstDefine.SuperAdminID
                });
                //增加四个基本功能菜单
                context.Menus.AddRange(
                   new Menu
                   {
                       Name = "组织机构管理",
                       Code = "Department",
                       SerialNumber = 1,
                       ParentId = roleMenuID,
                       Type = (int)AM_Enum.MenuTypeEnum.ChildMenu,
                       IsTopMenu = true,
                       Icon = "fa fa-link",
                       Url = "/Department/Index",
                       CreatorID = Utils.ConstDefine.SuperAdminID
                   },
                   new Menu
                   {
                       Name = "角色管理",
                       Code = "Role",
                       SerialNumber = 2,
                       ParentId = roleMenuID,
                       Type = (int)AM_Enum.MenuTypeEnum.ChildMenu,
                       IsTopMenu = true,
                       Icon = "fa fa-link",
                       Url = "/Role/Index",
                       CreatorID = Utils.ConstDefine.SuperAdminID
                   },
                   new Menu
                   {
                       Name = "用户管理",
                       Code = "User",
                       SerialNumber = 3,
                       ParentId = roleMenuID,
                       Type = (int)AM_Enum.MenuTypeEnum.ChildMenu,
                       IsTopMenu = true,
                       Icon = "fa fa-link",
                       Url = "/User/Index",
                       CreatorID = Utils.ConstDefine.SuperAdminID
                   }
                );
                #endregion

                #region 系统配置
                Guid plateSettingID = Guid.NewGuid();
                context.Menus.Add(new Menu()
                {
                    ID = plateSettingID,
                    Name = "系统配置",
                    Code = "PL_Setting",
                    SerialNumber = 2,
                    ParentId = Guid.Empty,
                    Icon = "fa fa-link",
                    Type = (int)AM_Enum.MenuTypeEnum.ParentMenu,
                    IsTopMenu = true,
                    Url = "#",
                    CreatorID = Utils.ConstDefine.SuperAdminID
                });
                context.Menus.AddRange(
                 new Menu
                 {
                     Name = "语言管理",
                     Code = "LanguageType",
                     SerialNumber = 1,
                     ParentId = plateSettingID,
                     Type = (int)AM_Enum.MenuTypeEnum.ChildMenu,
                     IsTopMenu = true,
                     Icon = "fa fa-link",
                     Url = "/LanguageType/Index",
                     CreatorID = Utils.ConstDefine.SuperAdminID
                 },
                 new Menu
                 {
                     Name = "参数配置",
                     Code = "PlateSetting",
                     SerialNumber = 2,
                     ParentId = plateSettingID,
                     Type = (int)AM_Enum.MenuTypeEnum.ChildMenu,
                     IsTopMenu = true,
                     Icon = "fa fa-link",
                     Url = "/PlateSetting/Index",
                     CreatorID = Utils.ConstDefine.SuperAdminID
                 },
                 new Menu
                 {
                     Name = "推送服务",
                     Code = "PushService",
                     SerialNumber = 3,
                     ParentId = plateSettingID,
                     Type = (int)AM_Enum.MenuTypeEnum.ChildMenu,
                     IsTopMenu = true,
                     Icon = "fa fa-link",
                     Url = "/PushService/Index",
                     CreatorID = Utils.ConstDefine.SuperAdminID
                 },
                 new Menu
                 {
                     Name = "网站配置",
                     Code = "SiteSetting",
                     SerialNumber = 4,
                     ParentId = plateSettingID,
                     Type = (int)AM_Enum.MenuTypeEnum.ChildMenu,
                     IsTopMenu = true,
                     Icon = "fa fa-link",
                     Url = "/SiteSetting/Index",
                     CreatorID = Utils.ConstDefine.SuperAdminID
                 },
                 new Menu
                 {
                     Name = "系统日志",
                     Code = "SysLog",
                     SerialNumber = 5,
                     ParentId = plateSettingID,
                     Type = (int)AM_Enum.MenuTypeEnum.ChildMenu,
                     IsTopMenu = true,
                     Icon = "fa fa-link",
                     Url = "/SysLog/Index",
                     CreatorID = Utils.ConstDefine.SuperAdminID
                 }
                 );
                #endregion

                #region 应用配置
                Guid appSettingID = Guid.NewGuid();
                context.Menus.Add(new Menu()
                {
                    ID = appSettingID,
                    Name = "应用配置",
                    Code = "PL_AppSetting",
                    SerialNumber = 3,
                    ParentId = Guid.Empty,
                    Icon = "fa fa-link",
                    Type = (int)AM_Enum.MenuTypeEnum.ParentMenu,
                    IsTopMenu = true,
                    Url = "#",
                    CreatorID = Utils.ConstDefine.SuperAdminID
                });

                context.Menus.AddRange(
                 new Menu
                 {
                     Name = "短信配置",
                     Code = "ShortMsgConfig",
                     SerialNumber = 1,
                     ParentId = appSettingID,
                     Type = (int)AM_Enum.MenuTypeEnum.ChildMenu,
                     IsTopMenu = true,
                     Icon = "fa fa-link",
                     Url = "/ShortMsgConfig/Index",
                     CreatorID = Utils.ConstDefine.SuperAdminID
                 },
                 new Menu
                 {
                     Name = "前台菜单",
                     Code = "UserMenu",
                     SerialNumber = 2,
                     ParentId = appSettingID,
                     Type = (int)AM_Enum.MenuTypeEnum.ChildMenu,
                     IsTopMenu = true,
                     Icon = "fa fa-link",
                     Url = "/UserMenu/Index",
                     CreatorID = Utils.ConstDefine.SuperAdminID
                 },
                 new Menu
                 {
                     Name = "短信模板",
                     Code = "ShortMsgTemplate",
                     SerialNumber = 3,
                     ParentId = appSettingID,
                     Type = (int)AM_Enum.MenuTypeEnum.ChildMenu,
                     IsTopMenu = true,
                     Icon = "fa fa-link",
                     Url = "/ShortMsgTemplate/Index",
                     CreatorID = Utils.ConstDefine.SuperAdminID
                 },
                 new Menu
                 {
                     Name = "邮件配置",
                     Code = "MailConfig",
                     SerialNumber = 4,
                     ParentId = appSettingID,
                     Type = (int)AM_Enum.MenuTypeEnum.ChildMenu,
                     IsTopMenu = true,
                     Icon = "fa fa-link",
                     Url = "/MailConfig/Index",
                     CreatorID = Utils.ConstDefine.SuperAdminID
                 },
                 new Menu
                 {
                     Name = "邮件模板",
                     Code = "MailTemplate",
                     SerialNumber = 5,
                     ParentId = appSettingID,
                     Type = (int)AM_Enum.MenuTypeEnum.ChildMenu,
                     IsTopMenu = true,
                     Icon = "fa fa-link",
                     Url = "/MailTemplate/Index",
                     CreatorID = Utils.ConstDefine.SuperAdminID
                 });
                #endregion

                #region 类容管理
                Guid cmsID = Guid.NewGuid();
                context.Menus.Add(new Menu()
                {
                    ID = cmsID,
                    Name = "类容管理",
                    Code = "CMS_All",
                    SerialNumber = 4,
                    ParentId = Guid.Empty,
                    Icon = "fa fa-link",
                    Type = (int)AM_Enum.MenuTypeEnum.ParentMenu,
                    IsTopMenu = false,
                    Url = "#",
                    CreatorID = Utils.ConstDefine.SuperAdminID
                });

                context.Menus.AddRange(
                new Menu
                {
                    Name = "文章类型",
                    Code = "ArticleType",
                    SerialNumber = 1,
                    ParentId = cmsID,
                    Type = (int)AM_Enum.MenuTypeEnum.ChildMenu,
                    IsTopMenu = false,
                    Icon = "fa fa-link",
                    Url = "/ArticleType/Index",
                    CreatorID = Utils.ConstDefine.SuperAdminID
                },
                new Menu
                {
                    Name = "文章列表",
                    Code = "Article",
                    SerialNumber = 2,
                    ParentId = cmsID,
                    Type = (int)AM_Enum.MenuTypeEnum.ChildMenu,
                    IsTopMenu = false,
                    Icon = "fa fa-link",
                    Url = "/Article/Index",
                    CreatorID = Utils.ConstDefine.SuperAdminID
                },
                new Menu
                {
                    Name = "Banner图",
                    Code = "Banner",
                    SerialNumber = 3,
                    ParentId = cmsID,
                    Type = (int)AM_Enum.MenuTypeEnum.ChildMenu,
                    IsTopMenu = false,
                    Icon = "fa fa-link",
                    Url = "/Banner/Index",
                    CreatorID = Utils.ConstDefine.SuperAdminID
                }

                );
                #endregion

                #region 初始化前台用户
                context.CustomerUser.Add(new CustomerUser()
                {
                    Mobile = "13430932507",
                    AuthenticationVerify = 1,
                    VerifyResult = 0,
                    NickName = "VIC",
                    RealName = "VIC LI",
                    Type = 1,
                    Status = 1,
                    PassWord = Core.Utils.SecurityHelper.EncryptDES("coresys"), //进行加密
                    TPassWord = Core.Utils.SecurityHelper.EncryptDES("coresys"), //进行加密
                    CreatorID = Utils.ConstDefine.SuperAdminID
                });
                #endregion

                context.SaveChanges();
            }
        }
    }
}
