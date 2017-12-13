using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Vic.Core.Application.Dtos
{
    public class UserDto : Vic.Core.Application.Dtos.BaseDto
    {
        		/// <summary>
		///
		/// </summary>
		public Guid DepartmentId { get; set; }
		/// <summary>
		///
		/// </summary>
		public string EMail { get; set; }
		/// <summary>
		///
		/// </summary>
		public bool IsDeleted { get; set; }
		/// <summary>
		///
		/// </summary>
		public DateTime LastLoginTime { get; set; }
		/// <summary>
		///
		/// </summary>
		public int LoginTimes { get; set; }
		/// <summary>
		///
		/// </summary>
		public string MobileNumber { get; set; }
		/// <summary>
		///
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///
		/// </summary>
		public string Password { get; set; }
		/// <summary>
		///
		/// </summary>
		public string Remarks { get; set; }
		/// <summary>
		///
		/// </summary>
		public string UserName { get; set; }

        public List<UserRoleDto> UserRoles { get; set; }
        public string DepartmentName { get; set; }
    }
}
