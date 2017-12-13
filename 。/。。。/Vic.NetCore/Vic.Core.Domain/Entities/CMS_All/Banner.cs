using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vic.Core.Domain.Entities
{
    [Table("T_CMS_Banner")]
    public class Banner:Entity
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Column("FSerialNumber")]
        public int SerialNumber { get; set; }
        /// <summary>
        /// 语言类型编号
        /// </summary>
        [Column("FLanguageTypeID")]
        [MaxLength(32)]
        public Guid LanguageTypeID { get; set; }
        /// <summary>
		///标题
		/// </summary>
        [Column("FTitle")]
        [MaxLength(100)]
        public string Title { get; set; }
        /// <summary>
        ///链接地址
        /// </summary>
        [Column("FUrl")]
        [MaxLength(200)]
        public string Url { get; set; }
        /// <summary>
        ///图片地址
        /// </summary>
        [Column("FImage")]
        [MaxLength(200)]
        public string Image { get; set; }
    }
}
