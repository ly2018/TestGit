using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vic.Core.Domain.Entities
{
    [Table("T_PST_CustomerUser")]
    public class CustomerUser:Entity
    {
        /// <summary>
        ///交易账号
        /// </summary>
        [Column("FTradeAccount")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TradeAccount { get; set; }
        /// <summary>
        ///手机号
        /// </summary>
        [Column("FMobile")]
        [MaxLength(20)]
        public string Mobile { get; set; }
        /// <summary>
        ///个人姓名
        /// </summary>
        [Column("FRealName")]
        [MaxLength(20)]
        public string RealName { get; set; }
        /// <summary>
        ///昵称
        /// </summary>
        [Column("FNickName")]
        [MaxLength(20)]
        public string NickName { get; set; }
        /// <summary>
        ///邮件
        /// </summary>
        [Column("FEmail")]
        [MaxLength(50)]
        public string Email { get; set; }
        /// <summary>
        ///身份证号
        /// </summary>
        [Column("FIDCard")]
        [MaxLength(18)]
        public string IDCard { get; set; }
        /// <summary>
        ///推荐人ID
        /// </summary>
        [Column("FRecommendID")]
        [MaxLength(32)]
        public Guid RecommendID { get; set; }
        /// <summary>
        ///1承销商2经纪人
        /// </summary>
        [Column("FType")]
        public int Type { get; set; }
        /// <summary>
        ///手持身份证照片
        /// </summary>
        [Column("FCardHandHeldPhoto")]
        [MaxLength(100)]
        public string CardHandHeldPhoto { get; set; }
        /// <summary>
        ///身份证正面照片
        /// </summary>
        [Column("FCardFrontPhoto")]
        [MaxLength(100)]
        public string CardFrontPhoto { get; set; }
        /// <summary>
        ///身份证反面照片
        /// </summary>
        [Column("FCardNegativePhoto")]
        [MaxLength(100)]
        public string CardNegativePhoto { get; set; }
        /// <summary>
        ///认证审核 1已注册2提交资料3审核失败4审核通过5驳回
        /// </summary>
        [Column("FAuthenticationVerify")]
        public int AuthenticationVerify { get; set; }
        /// <summary>
        /// 审核结果 1、身份信息与图像不符；2、图像模糊；3、不合格的身份证图像 ; 4、管理员驳回审核 5、审核通过
        /// </summary>
        [Column("FVerifyResult")]
        public int VerifyResult { get; set; }
        /// <summary>
        ///1正常2冻结3删除4拉黑
        /// </summary>
        [Column("FStatus")]
        public int Status { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [Column("FPassWord")]
        [MaxLength(200)]
        public string PassWord { get; set; }
        /// <summary>
        /// 资金密码
        /// </summary>
        [Column("FTPassWord")]
        [MaxLength(200)]
        public string TPassWord { get; set; }
    }
}
