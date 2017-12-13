using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vic.Core.Domain.Entities
{
    [Table("T_AM_UserRole")]
    public class UserRole
    {
        [Column("FUserId")]
        [MaxLength(32)]
        [Required]
        [ForeignKey("UserId-Key")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [Column("FRoleId")]
        [MaxLength(32)]
        [Required]
        [ForeignKey("RoleId-Key")]
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }

    }
}
