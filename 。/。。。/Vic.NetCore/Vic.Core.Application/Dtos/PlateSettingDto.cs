using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Vic.Core.Application.Dtos
{
    public class PlateSettingDto : Vic.Core.Application.Dtos.BaseDto
    {
        		/// <summary>
		///键
		/// </summary>
		public string Key { get; set; }
		/// <summary>
		///标签
		/// </summary>
		public string Tag { get; set; }
		/// <summary>
		///值
		/// </summary>
		public string Value { get; set; }

    }
}
