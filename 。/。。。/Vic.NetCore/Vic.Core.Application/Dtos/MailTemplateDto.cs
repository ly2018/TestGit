using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Vic.Core.Application.Dtos
{
    public class MailTemplateDto : Vic.Core.Application.Dtos.BaseDto
    {
        /// <summary>
        ///调用枚举
        /// </summary>
        public string CallEnumKey { get; set; }
        /// <summary>
        ///内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        ///是否默认
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        ///邮件标题
        /// </summary>
        public string MailTitle { get; set; }
        /// <summary>
        ///标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 语言类型编号
        /// </summary>
        public Guid LanguageTypeID { get; set; }
        public string LanguageTypeName { get; set; }
    }
}
