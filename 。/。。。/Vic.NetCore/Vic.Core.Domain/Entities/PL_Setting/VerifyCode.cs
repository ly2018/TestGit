using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vic.Core.Domain.Entities
{
    [Table("T_PL_VerifyCode")]
    public class VerifyCode:Entity
    {
        /// <summary>
		///手机
		/// </summary>
        [Column("FPhone")]
        [MaxLength(20)]
        public  string Phone { get; set; }
        /// <summary>
        ///发送时间
        /// </summary>
        [Column("FSendTime")]
        public  DateTime SendTime { get; set; }
        /// <summary>
        ///验证码
        /// </summary>
        [Column("FCode")]
        [MaxLength(10)]
        public  string Code { get; set; }
        /// <summary>
        ///验证时间
        /// </summary>
        [Column("FVerifyTime")]
        public  DateTime? VerifyTime { get; set; }
        /// <summary>
        ///有效时间长(秒)
        /// </summary>
        [Column("FValidTimeLen")]
        public  short ValidTimeLen { get; set; }
        /// <summary>
        ///有效验证时间时间
        /// </summary>
        [Column("FValidTime")]
        public  DateTime? ValidTime { get; set; }
        /// <summary>
        ///验证标志（详见枚举）：0-待验证 1-验证成功 2-验证失败 3-超时失效 
        /// </summary>
        [Column("FVerifyFlag")]
        public  short VerifyFlag { get; set; }
        /// <summary>
        ///业务类型（详见枚举）：0-用户注册 1-修改登录密码
        /// </summary>
        [Column("FBussinessType")]
        public  short BussinessType { get; set; }
    }
}
