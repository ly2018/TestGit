using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Vic.Core.Application.Dtos
{
    public class ShortMsgConfigDto : Vic.Core.Application.Dtos.BaseDto
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
		///APIKey
		/// </summary>
		public string ApiKey { get; set; }
		/// <summary>
		///ApiSecret
		/// </summary>
		public string ApiSecret { get; set; }
		/// <summary>
		///链接地址
		/// </summary>
		public string ApiUrl { get; set; }
		/// <summary>
		///调用枚举值
		/// </summary>
		public string CallEnumKey { get; set; }
		/// <summary>
		///是否默认
		/// </summary>
		public bool IsDefault { get; set; }

    }
}
