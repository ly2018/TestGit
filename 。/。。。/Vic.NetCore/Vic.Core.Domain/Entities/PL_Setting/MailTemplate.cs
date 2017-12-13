using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vic.Core.Domain.Entities
{
    [Table("T_PL_MailTemplate")]
    public class MailTemplate:Entity
    {
        /// <summary>
		///是否系统默认
		/// </summary>
        [Column("FIsDefault")]
        public  bool IsDefault { get; set; }
        /// <summary>
        /// 语言类型编号
        /// </summary>
        [Column("FLanguageTypeID")]
        [MaxLength(32)]
        public Guid LanguageTypeID { get; set; }
        /// <summary>
        ///模板标题
        /// </summary>
        [Column("FTitle")]
        [MaxLength(50)]
        [Required]
        public  string Title { get; set; }
        /// <summary>
        ///调用名称
        /// </summary>
        [Column("FCallEnumKey")]
        [MaxLength(50)]
        [Required]
        public  string CallEnumKey { get; set; }
        /// <summary>
        ///邮件标题
        /// </summary>
        [Column("FMailTitle")]
        [MaxLength(500)]
        [Required]
        public  string MailTitle { get; set; }
        /// <summary>
        ///邮件类容
        /// </summary>
        [Column("FContent")]
        [MaxLength(8000)]
        [Required]
        public  string Content { get; set; }
    }
}
