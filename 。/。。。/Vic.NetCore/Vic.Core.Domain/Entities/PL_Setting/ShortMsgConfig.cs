using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vic.Core.Domain.Entities
{
    [Table("T_PL_ShortMsgConfig")]
    public class ShortMsgConfig : Entity
    {
        /// <summary>
		///调用名称
		/// </summary>
        [Column("FCallEnumKey")]
        [MaxLength(50)]
        public  string CallEnumKey { get; set; }
        /// <summary>
        ///是否系统默认
        /// </summary>
        [Column("FIsDefault")]
        public  bool IsDefault { get; set; }
        /// <summary>
        ///短信API地址
        /// </summary>
        [Column("FApiUrl")]
        [MaxLength(200)]
        [Required]
        public  string ApiUrl { get; set; }
        /// <summary>
        ///短信接口ApiSecret
        /// </summary>
        [Column("FApiSecret")]
        [MaxLength(200)]
        public string ApiSecret { get; set; }
        /// <summary>
        ///短信接口ApiKey
        /// </summary>
        [Column("FApiKey")]
        [MaxLength(200)]
        public string ApiKey { get; set; }
        /// <summary>
        ///调用名称
        /// </summary>
        [Column("FAccountName")]
        [MaxLength(50)]
        public  string AccountName { get; set; }
        /// <summary>
        ///平台登录密码
        /// </summary>
        [Column("FAccountPwd")]
        [MaxLength(200)]
        public  string AccountPwd { get; set; }
    }
}
