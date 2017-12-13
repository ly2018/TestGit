using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vic.Core.Domain.Entities
{
    [Table("T_CMS_UserMenu")]
    public class UserMenu : Entity
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        [Column("FParentId")]
        [MaxLength(32)]
        public Guid ParentId { get; set; }
        /// <summary>
        /// 语言类型编号
        /// </summary>
        [Column("FLanguageTypeID")]
        [MaxLength(32)]
        public Guid LanguageTypeID { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [Column("FSerialNumber")]
        public int SerialNumber { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [Column("FName")]
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 菜单编码
        /// </summary>
        [Column("FCode")]
        [MaxLength(20)]
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        [Column("FUrl")]
        [MaxLength(150)]
        public string Url { get; set; }

        /// <summary>
        /// 类型：0导航菜单；1操作按钮。
        /// </summary>
        [Column("FType")]
        public int Type { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        [Column("FIcon")]
        [MaxLength(10)]
        public string Icon { get; set; }

        /// <summary>
        /// 菜单备注
        /// </summary>
        [Column("FRemarks")]
        [MaxLength(200)]
        public string Remarks { get; set; }
    }
}
