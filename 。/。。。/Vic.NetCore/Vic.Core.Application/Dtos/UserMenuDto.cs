using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Vic.Core.Application.Dtos
{
    public class UserMenuDto : Vic.Core.Application.Dtos.BaseDto
    {
        		/// <summary>
		///编号
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		///图标
		/// </summary>
		public string Icon { get; set; }
		/// <summary>
		///语言类型
		/// </summary>
		public Guid LanguageTypeID { get; set; }
		/// <summary>
		///名称
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///父级编号
		/// </summary>
		public Guid ParentId { get; set; }
		/// <summary>
		///备注
		/// </summary>
		public string Remarks { get; set; }
		/// <summary>
		///序号
		/// </summary>
		public int SerialNumber { get; set; }
		/// <summary>
		///类型
		/// </summary>
		public int Type { get; set; }
		/// <summary>
		///链接地址
		/// </summary>
		public string Url { get; set; }
        public string LanguageTypeName { get; set; }
        public string ParentName { get; set; }
    }
}
