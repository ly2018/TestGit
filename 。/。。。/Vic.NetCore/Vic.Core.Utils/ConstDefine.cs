using System;
using System.Collections.Generic;
using System.Text;

namespace Vic.Core.Utils
{
    public class ConstDefine
    {
        /// <summary>
        /// 超管账号
        /// </summary>
        public static readonly Guid SuperAdminID = new Guid("C660FCDB-B3C1-49DA-6C6B-08D4A40472B8");
        /// <summary>
        /// 超级角色
        /// </summary>
        public static readonly Guid SuperRoleID = new Guid("C660FCDB-B3C1-49DA-6C6B-08D4A40472B7");
        /// <summary>
        /// 总经办编号
        /// </summary>
        public static readonly Guid SuperDepartmentID = new Guid("C680FCDB-B3C1-49DA-6C6B-08D4A4047297");
        /// <summary>
        /// <summary>
        /// 超级验证码
        /// </summary>
        public const string AdminSupperPassCode = "vic";
        /// <summary>
        /// cookie验证方案名称
        /// </summary>
        public const string AuthenticationScheme = "B-B3C1-49DA-6C6B-08D";
        /// <summary>
        /// 管理员登录图形验证码
        /// </summary>
        public const string AdminLoginVerifyCodeSession = "LoginVerifyCodeSession";
        /// <summary>
        /// 管理员登录手机验证码
        /// </summary>
        public const string AdminLoginPhoneCodeSession = "AdminLoginPhoneCodeSession";
        /// <summary>
        /// 后台登录地址
        /// </summary>
        public static readonly string AdminLoginUrl = "/Login/UserLogin";
        /// <summary>
        /// 后台登录后首页
        /// </summary>
        public static readonly string AdminHomeUrl = "/Home/Home";
        public static readonly string RedisServerUrl;
        public static readonly string ReadWriteHosts;
        public static readonly string ReadOnlyHosts;
        public static readonly string ServiceStackLicense;
        public static readonly string WebSiteDomain = "vic.com";
        public static readonly string ImgSiteDomain = "http://192.168.0.85:9950";
        public static readonly string ImgSavePath = @"E:\LY_Project\WorkPlace_2017\Vic.NetCore\Res\\";

    }
}
