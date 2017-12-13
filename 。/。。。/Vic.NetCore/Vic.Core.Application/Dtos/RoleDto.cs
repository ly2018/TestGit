using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Vic.Core.Application.Dtos
{
    public class RoleDto : Vic.Core.Application.Dtos.BaseDto
    {
        /// <summary>
        ///
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string Remarks { get; set; }

        public string RoleValue { get; set; }

        public ICollection<RoleMenuDto> RoleMenus { get; set; }

    }
}
