using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Vic.Core.Application.Dtos
{
    public class ArticleDto : Vic.Core.Application.Dtos.BaseDto
    {
        		/// <summary>
		///文章类型
		/// </summary>
		public Guid ArticleTypeID { get; set; }
		/// <summary>
		///作者
		/// </summary>
		public string Author { get; set; }
		/// <summary>
		///浏览量
		/// </summary>
		public int BrowseNum { get; set; }
		/// <summary>
		///详情
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		///描述
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		///图片
		/// </summary>
		public string Images { get; set; }
		/// <summary>
		///语言类型
		/// </summary>
		public Guid LanguageTypeID { get; set; }
		/// <summary>
		///发布日期
		/// </summary>
		public DateTime PublishDate { get; set; }
		/// <summary>
		///备注
		/// </summary>
		public string Remark { get; set; }
		/// <summary>
		///SEO标题
		/// </summary>
		public string SeoTitle { get; set; }
		/// <summary>
		///序号
		/// </summary>
		public int SerialNumber { get; set; }
		/// <summary>
		///来源
		/// </summary>
		public string Source { get; set; }
		/// <summary>
		///标题
		/// </summary>
		public string Title { get; set; }
        public string LanguageTypeName { get; set; }
        public string ArticleTypeName { get; set; }
    }
}
