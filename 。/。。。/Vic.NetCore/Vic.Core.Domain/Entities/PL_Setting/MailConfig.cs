using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vic.Core.Domain.Entities
{
    [Table("T_PL_MailConfig")]
    public class MailConfig : Entity
    {
        /// <summary>
		///是否系统默认
		/// </summary>
        [Column("FIsDefault")]
        public  bool IsDefault { get; set; }
        /// <summary>
        ///STMP服务器
        /// </summary>
        [Column("FStmp")]
        [MaxLength(50)]
        [Required]
        public  string Stmp { get; set; }
        /// <summary>
        ///调用名称
        /// </summary>
        [Column("FCallEnumKey")]
        [MaxLength(50)]
        [Required]
        public  string CallEnumKey { get; set; }
        /// <summary>
        ///SMTP端口 
        /// </summary>
        [Column("FPort")]
        [MaxLength(10)]
        [Required]
        public  short Port { get; set; }
        /// <summary>
        ///邮件类容
        /// </summary>
        [Column("FContent")]
        [MaxLength(8000)]
        [Required]
        public  string Content { get; set; }
        /// <summary>
        ///发件箱
        /// </summary>
        [Column("FMailFrom")]
        [MaxLength(50)]
        [Required]
        public  string MailFrom { get; set; }
        /// <summary>
        ///收件箱
        /// </summary>
        [Column("FMailTo")]
        [MaxLength(50)]
        [Required]
        public  string MailTo { get; set; }
        /// <summary>
        ///调用名称
        /// </summary>
        [Column("FAccountName")]
        [MaxLength(50)]
        [Required]
        public  string AccountName { get; set; }
        /// <summary>
        ///平台登录密码
        /// </summary>
        [Column("FAccountPwd")]
        [MaxLength(200)]
        [Required]
        public  string AccountPwd { get; set; }
        /// <summary>
        ///发件人昵称
        /// </summary>
        [Column("FNickName")]
        [MaxLength(50)]
        [Required]
        public  string NickName { get; set; }
    }
}
