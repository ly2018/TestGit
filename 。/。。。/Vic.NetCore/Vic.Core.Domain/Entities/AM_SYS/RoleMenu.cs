using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vic.Core.Domain.Entities
{
    [Table("T_AM_RoleMenu")]
    public class RoleMenu
    {
        [Column("FRoleId")]
        [MaxLength(32)]
        [Required]
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }

        [Column("FMenuId")]
        [MaxLength(32)]
        [Required]
        public Guid MenuId { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
