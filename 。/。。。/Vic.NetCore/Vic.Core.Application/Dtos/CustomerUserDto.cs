using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Vic.Core.Application.Dtos
{
    public class CustomerUserDto : Vic.Core.Application.Dtos.BaseDto
    {
        		/// <summary>
		///认证状态枚举
		/// </summary>
		public int AuthenticationVerify { get; set; }
		/// <summary>
		///身份证正面
		/// </summary>
		public string CardFrontPhoto { get; set; }
		/// <summary>
		///身份证手持
		/// </summary>
		public string CardHandHeldPhoto { get; set; }
		/// <summary>
		///身份证反面
		/// </summary>
		public string CardNegativePhoto { get; set; }
		/// <summary>
		///邮箱
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		///身份证号
		/// </summary>
		public string IDCard { get; set; }
		/// <summary>
		///手机号
		/// </summary>
		public string Mobile { get; set; }
		/// <summary>
		///昵称
		/// </summary>
		public string NickName { get; set; }
		/// <summary>
		///登录密码
		/// </summary>
		public string PassWord { get; set; }
		/// <summary>
		///姓名
		/// </summary>
		public string RealName { get; set; }
		/// <summary>
		///推荐人
		/// </summary>
		public Guid RecommendID { get; set; }
		/// <summary>
		///用户状态
		/// </summary>
		public int Status { get; set; }
		/// <summary>
		///资金密码
		/// </summary>
		public string TPassWord { get; set; }
		/// <summary>
		///交易账户
		/// </summary>
		public int TradeAccount { get; set; }
		/// <summary>
		///会员类型
		/// </summary>
		public int Type { get; set; }
		/// <summary>
		///认证审核结果
		/// </summary>
		public int VerifyResult { get; set; }

    }
}
