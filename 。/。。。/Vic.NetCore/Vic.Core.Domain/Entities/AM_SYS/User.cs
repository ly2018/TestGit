using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vic.Core.Domain.Entities
{
    [Table("T_AM_User")]
    public class User : Entity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Column("FUserName")]
        [MaxLength(50)]
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Column("FPassword")]
        [MaxLength(200)]
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        [Column("FName")]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        [Column("FEMail")]
        [MaxLength(50)]
        public string EMail { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Column("FMobileNumber")]
        [MaxLength(20)]
        public string MobileNumber { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("FRemarks")]
        [MaxLength(200)]
        public string Remarks { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        [Column("FLastLoginTime")]
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        [Column("FLoginTimes")]
        public int LoginTimes { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [Column("FDepartmentId")]
        [MaxLength(32)]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        [Column("FIsDeleted")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 所属部门实体
        /// </summary>
        public virtual Department Department { get; set; }

        /// <summary>
        /// 角色集合
        /// </summary>

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
