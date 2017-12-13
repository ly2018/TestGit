using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Vic.Core.Application.Dtos
{
    public class MenuDto : Vic.Core.Application.Dtos.BaseDto
    {
        		/// <summary>
		///
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		///
		/// </summary>
		public string Icon { get; set; }
		/// <summary>
		///
		/// </summary>
		public bool IsTopMenu { get; set; }
		/// <summary>
		///
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///
		/// </summary>
		public Guid ParentId { get; set; }
		/// <summary>
		///
		/// </summary>
		public string Remarks { get; set; }
		/// <summary>
		///
		/// </summary>
		public int SerialNumber { get; set; }
		/// <summary>
		///
		/// </summary>
		public int Type { get; set; }
		/// <summary>
		///
		/// </summary>
		public string Url { get; set; }
        public string ParentName { get; set; }
    }
}
