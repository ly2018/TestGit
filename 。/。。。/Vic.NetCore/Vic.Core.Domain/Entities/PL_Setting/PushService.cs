using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vic.Core.Domain.Entities
{
    [Table("T_PL_PushService")]
    public class PushService : Entity
    {
        /// <summary>
		///FIP
		/// </summary>
        [Column("FIP")]
        [MaxLength(20)]
        [Required]
        public  string IP { get; set; }
        /// <summary>
        ///FPort
        /// </summary>
        [Column("FPort")]
        [MaxLength(10)]
        [Required]
        public  int Port { get; set; }
    }
}
