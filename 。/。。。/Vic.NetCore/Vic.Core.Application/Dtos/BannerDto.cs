using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Vic.Core.Application.Dtos
{
    public class BannerDto : Vic.Core.Application.Dtos.BaseDto
    {
        		/// <summary>
		///图片地址
		/// </summary>
		public string Image { get; set; }
		/// <summary>
		///语言类型
		/// </summary>
		public Guid LanguageTypeID { get; set; }
		/// <summary>
		///序号
		/// </summary>
		public int SerialNumber { get; set; }
		/// <summary>
		///标题
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		///链接地址
		/// </summary>
		public string Url { get; set; }
        public string LanguageTypeName { get; set; }
    }
}
