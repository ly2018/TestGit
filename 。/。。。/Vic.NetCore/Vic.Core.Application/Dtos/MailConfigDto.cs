using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Vic.Core.Application.Dtos
{
    public class MailConfigDto : Vic.Core.Application.Dtos.BaseDto
    {
        		/// <summary>
		///登录账号
		/// </summary>
		public string AccountName { get; set; }
		/// <summary>
		///登录密码
		/// </summary>
		public string AccountPwd { get; set; }
		/// <summary>
		///调用枚举
		/// </summary>
		public string CallEnumKey { get; set; }
		/// <summary>
		///内容
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		///是否默认
		/// </summary>
		public bool IsDefault { get; set; }
		/// <summary>
		///发件箱
		/// </summary>
		public string MailFrom { get; set; }
		/// <summary>
		///收件箱
		/// </summary>
		public string MailTo { get; set; }
		/// <summary>
		///昵称
		/// </summary>
		public string NickName { get; set; }
		/// <summary>
		///端口
		/// </summary>
		public short Port { get; set; }
		/// <summary>
		///Stmp服务器
		/// </summary>
		public string Stmp { get; set; }

    }
}
