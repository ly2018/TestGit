using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vic.Core.Domain.Entities
{
    [Table("T_AM_Role")]
    public class Role : Entity
    {
        [Column("FCode")]
        [MaxLength(20)]
        [Required]
        public string Code { get; set; }

        [Column("FName")]
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [Column("FRemarks")]
        [MaxLength(200)]
        public string Remarks { get; set; }

        public virtual ICollection<RoleMenu> RoleMenus { get; set; }
    }
}
