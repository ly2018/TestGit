using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Vic.Core.Application.Dtos
{
    public class DepartmentDto : Vic.Core.Application.Dtos.BaseDto
    {
        		/// <summary>
		///
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		///
		/// </summary>
		public string ContactNumber { get; set; }
		/// <summary>
		///
		/// </summary>
		public bool IsDeleted { get; set; }
		/// <summary>
		///
		/// </summary>
		public string Manager { get; set; }
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
        public string DepartmentName { get; set; }
    }
}
