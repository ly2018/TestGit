using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vic.Core.Domain.Entities
{
    /// <summary>
    /// 功能菜单实体
    /// </summary>
    [Table("T_AM_Menu")]
    public class Menu : Entity
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        [Column("FParentId")]
        [MaxLength(32)]
        public Guid ParentId { get; set; }

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
        [MaxLength(100)]
        public string Url { get; set; }
        /// <summary>
        /// 类型：AM_Enum.MenuTypeEnum 枚举
        /// </summary>
        [Column("FType")]
        public int Type { get; set; }
        /// <summary>
        /// 是否顶部菜单
        /// </summary>
        [Column("FIsTopMenu")]
        public bool IsTopMenu { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary>
        [Column("FIcon")]
        [MaxLength(50)]
        public string Icon { get; set; }

        /// <summary>
        /// 菜单备注
        /// </summary>
        [Column("FRemarks")]
        [MaxLength(200)]
        public string Remarks { get; set; }
    }
}
