using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Vic.Core.Application.Dtos
{
    public class RoleMenuDto
    {
        /// <summary>
        ///
        /// </summary>
        public Guid RoleId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Guid MenuId { get; set; }

    }
}
