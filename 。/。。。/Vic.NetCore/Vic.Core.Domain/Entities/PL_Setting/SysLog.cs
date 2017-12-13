using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vic.Core.Domain.Entities
{
    [Table("T_PL_SysLog")]
    public class SysLog : Entity
    {
        /// <summary>
		///操作类型
		/// </summary>
        [Column("FLogType")]
        [MaxLength(50)]
        [Required]
        public  string LogType { get; set; }
        /// <summary>
		///操作用户IP
		/// </summary>
        [Column("FIP")]
        [MaxLength(20)]
        [Required]
        public string IP { get; set; }
        /// <summary>
        ///操作类容--格式：用户{0}，充值{1}，结果（成功/失败）
        /// </summary>
        [Column("FContent")]
        [MaxLength(1000)]
        [Required]
        public  string Content { get; set; }
        /// <summary>
        ///安全级别
        /// </summary>
        [Column("FSecurityLevel")]
        [MaxLength(5)]
        [Required]
        public  string SecurityLevel { get; set; }
        /// <summary>
        ///操作币量
        /// </summary>
        [Column("FNum")]
        public  Decimal Num { get; set; }
        /// <summary>
        ///是否管理员操作
        /// </summary>
        [Column("FIsAdmin")]
        [Required]
        public  bool IsAdmin { get; set; }
    }
}
