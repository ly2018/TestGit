using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vic.Core.Domain.Entities
{
    [Table("T_CMS_ArticleType")]
    public class ArticleType : Entity
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Column("FSerialNumber")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SerialNumber { get; set; }
        /// <summary>
        /// 语言类型编号
        /// </summary>
        [Column("FLanguageTypeID")]
        [MaxLength(32)]
        public Guid LanguageTypeID { get; set; }
        /// <summary>
        ///名称
        /// </summary>
        [Column("FTitle")]
        [MaxLength(100)]
        public string Title { get; set; }
        /// <summary>
        ///名称
        /// </summary>
        [Column("FUrl")]
        [MaxLength(200)]
        public string Url { get; set; }
        /// <summary>
        ///图片
        /// </summary>
        [Column("FImages")]
        [MaxLength(200)]
        public string Images { get; set; }
    }
}
