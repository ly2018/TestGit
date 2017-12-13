using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Vic.Core.Application.Dtos
{
    public class SysLogDto : Vic.Core.Application.Dtos.BaseDto
    {
        		/// <summary>
		///内容
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		///IP
		/// </summary>
		public string IP { get; set; }
		/// <summary>
		///是否管理员
		/// </summary>
		public bool IsAdmin { get; set; }
		/// <summary>
		///日志类型枚举
		/// </summary>
		public string LogType { get; set; }
		/// <summary>
		///数量
		/// </summary>
		public Decimal Num { get; set; }
		/// <summary>
		///安全级别
		/// </summary>
		public string SecurityLevel { get; set; }

    }
}
