using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Vic.Core.Application.Dtos
{
    public class BaseDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
		///创建者
		/// </summary>
        public Guid CreatorID { get; set; }
        /// <summary>
        ///创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        ///最后更新用户ID
        /// </summary>
        public Guid? LastUpdateUserID { get; set; }
        /// <summary>
        ///最后更新时间
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }
    }
}
