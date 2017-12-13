using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Vic.Core.Utils.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "图形验证码不能为空")]
        public string VerifyCode { get; set; }

        [Required(ErrorMessage = "手机验证码不能为空")]
        public string PhoneCode { get; set; }
    }
}
