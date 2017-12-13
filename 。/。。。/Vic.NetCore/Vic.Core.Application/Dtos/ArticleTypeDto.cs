using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Vic.Core.Application.Dtos
{
    public class ArticleTypeDto : Vic.Core.Application.Dtos.BaseDto
    {
        		/// <summary>
		///图片
		/// </summary>
		public string Images { get; set; }
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
