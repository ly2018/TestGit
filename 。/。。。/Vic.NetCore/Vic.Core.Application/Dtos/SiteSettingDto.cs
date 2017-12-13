using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Vic.Core.Application.Dtos
{
    public class SiteSettingDto : Vic.Core.Application.Dtos.BaseDto
    {
        		/// <summary>
		///编码
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		///详细地址
		/// </summary>
		public string ContractAddress { get; set; }
		/// <summary>
		///邮箱
		/// </summary>
		public string ContractEmail { get; set; }
		/// <summary>
		///联系电话
		/// </summary>
		public string ContractPhone { get; set; }
		/// <summary>
		///QQ
		/// </summary>
		public string ContractQQ { get; set; }
		/// <summary>
		///是否后台站点
		/// </summary>
		public bool IsAdmin { get; set; }
		/// <summary>
		///语言类型
		/// </summary>
		public Guid LanguageTypeID { get; set; }
		/// <summary>
		///网站Logo
		/// </summary>
		public string Logo { get; set; }
		/// <summary>
		///网站小Logo
		/// </summary>
		public string LogoIco { get; set; }
		/// <summary>
		///二维码
		/// </summary>
		public string QrCode { get; set; }
		/// <summary>
		///版权信息
		/// </summary>
		public string SiteRight { get; set; }
		/// <summary>
		///网站标题
		/// </summary>
		public string Title { get; set; }
        /// <summary>
        /// 语言名称
        /// </summary>
        public string LanguageTypeName { get; set; }
    }
}
