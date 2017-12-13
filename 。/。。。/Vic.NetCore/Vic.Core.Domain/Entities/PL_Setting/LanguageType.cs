using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vic.Core.Domain.Entities
{
    [Table("T_PL_LanguageType")]
    public class LanguageType:Entity
    {
        /// <summary>
        ///语言名称
        /// </summary>
        [Column("FTitle")]
        [MaxLength(50)]
        public string Title { get; set; }
        /// <summary>
        ///语言编号
        /// </summary>
        [Column("FCode")]
        [MaxLength(50)]
        public string Code { get; set; }
        /// <summary>
        ///语言图片
        /// </summary>
        [Column("FImages")]
        [MaxLength(200)]
        public string Images { get; set; }
    }
}
