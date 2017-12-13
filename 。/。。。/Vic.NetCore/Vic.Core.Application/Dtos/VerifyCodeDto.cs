using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Vic.Core.Application.Dtos
{
    public class VerifyCodeDto : Vic.Core.Application.Dtos.BaseDto
    {
        		/// <summary>
		///业务类型枚举
		/// </summary>
		public short BussinessType { get; set; }
		/// <summary>
		///验证码
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		///手机号
		/// </summary>
		public string Phone { get; set; }
		/// <summary>
		///发送时间
		/// </summary>
		public DateTime SendTime { get; set; }
		/// <summary>
		///有效时间
		/// </summary>
		public DateTime ValidTime { get; set; }
		/// <summary>
		///有效时长（秒）
		/// </summary>
		public short ValidTimeLen { get; set; }
		/// <summary>
		///验证状态枚举
		/// </summary>
		public short VerifyFlag { get; set; }
		/// <summary>
		///验证时间
		/// </summary>
		public DateTime VerifyTime { get; set; }

    }
}
