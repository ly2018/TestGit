using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vic.Core.Domain.Entities
{
    [Table("T_PL_PlateSetting")]
    public class PlateSetting:Entity
    {
        /// <summary>
		///键
		/// </summary>
        [Column("FKey")]
        [MaxLength(50)]
        [Required]
        public  string Key { get; set; }
        /// <summary>
        ///值
        /// </summary>
        [Column("FValue")]
        [MaxLength(200)]
        [Required]
        public  string Value { get; set; }
        /// <summary>
        ///标签
        /// </summary>
        [Column("FTag")]
        [MaxLength(50)]
        [Required]
        public  string Tag { get; set; }
    }
}
