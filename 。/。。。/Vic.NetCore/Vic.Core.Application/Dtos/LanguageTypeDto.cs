using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Vic.Core.Application.Dtos
{
    public class LanguageTypeDto : Vic.Core.Application.Dtos.BaseDto
    {
        		/// <summary>
		///编号
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		///图片
		/// </summary>
		public string Images { get; set; }
		/// <summary>
		///名称
		/// </summary>
		public string Title { get; set; }

    }
}
