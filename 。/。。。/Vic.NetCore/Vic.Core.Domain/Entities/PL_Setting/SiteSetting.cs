using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vic.Core.Domain.Entities
{
    /// <summary>
    /// 站点配置
    /// </summary>
    [Table("T_PL_SiteSetting")]
    public class SiteSetting : Entity
    {
        /// <summary>
        ///站点编号
        /// </summary>
        [Column("FCode")]
        [MaxLength(20)]
        public string Code { get; set; }
        /// <summary>
        ///站点名称
        /// </summary>
        [Column("FTitle")]
        [MaxLength(50)]
        public string Title { get; set; }
        /// <summary>
        /// 语言类型编号
        /// </summary>
        [Column("FLanguageTypeID")]
        [MaxLength(32)]
        public Guid LanguageTypeID { get; set; }
        /// <summary>
        ///是否后台系统
        /// </summary>
        [Column("FIsAdmin")]
        public bool IsAdmin { get; set; }
        /// <summary>
        ///Logo图片
        /// </summary>
        [Column("FLogo")]
        [MaxLength(50)]
        public string Logo { get; set; }
        /// <summary>
        ///LogoIco图片
        /// </summary>
        [Column("FLogoIco")]
        [MaxLength(50)]
        public string LogoIco { get; set; }
        /// <summary>
        ///QrCode图片
        /// </summary>
        [Column("FQrCode")]
        [MaxLength(50)]
        public string QrCode { get; set; }
        /// <summary>
        ///联系电话
        /// </summary>
        [Column("FContractPhone")]
        [MaxLength(50)]
        public string ContractPhone { get; set; }
        /// <summary>
        ///联系Email
        /// </summary>
        [Column("FContractEmail")]
        [MaxLength(50)]
        public string ContractEmail { get; set; }
        /// <summary>
        ///联系QQ
        /// </summary>
        [Column("FContractQQ")]
        [MaxLength(50)]
        public string ContractQQ { get; set; }
        /// <summary>
        ///联系地址
        /// </summary>
        [Column("FContractAddress")]
        [MaxLength(200)]
        public string ContractAddress { get; set; }
        /// <summary>
        ///版权信息
        /// </summary>
        [Column("FSiteRight")]
        [MaxLength(200)]
        public string SiteRight { get; set; }
    }
}