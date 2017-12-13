using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vic.Core.Domain.Entities
{
    /// <summary>
    /// 部门实体
    /// </summary>
    [Table("T_AM_Department")]
    public class Department : Entity
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        [Column("FName")]
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 部门编号
        /// </summary>
        [Column("FCode")]
        [MaxLength(20)]
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 部门负责人
        /// </summary>
        [Column("FManager")]
        [MaxLength(50)]
        public string Manager { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Column("FContactNumber")]
        [MaxLength(20)]
        public string ContactNumber { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("FRemarks")]
        [MaxLength(200)]
        public string Remarks { get; set; }

        /// <summary>
        /// 父级部门ID
        /// </summary>
        [Column("FParentId")]
        [MaxLength(32)]
        public Guid ParentId { get; set; }
        /// <summary>
        /// 是否已删除
        /// </summary>
        [Column("FIsDeleted")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 包含用户
        /// </summary>
        public virtual ICollection<User> Users { get; set; }

    }
}
