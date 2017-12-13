using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.ComponentModel;

namespace Vic.Core.Domain.Entities
{
    /// <summary>
    /// 泛型实体基类
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public abstract class Entity<TPrimaryKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column("FID")]
        [MaxLength(32)]
        [Required]
        public virtual TPrimaryKey ID { get; set; }
        /// <summary>
		///创建者
		/// </summary>
        [Column("FCreatorID")]
        [MaxLength(32)]
        [Required]
        public virtual Guid CreatorID { get; set; }
        /// <summary>
        ///创建时间
        /// </summary>
        [Column("FCreateTime")]
        [Required]
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        ///最后更新用户ID
        /// </summary>
        [Column("FLastUpdateUserID")]
        [MaxLength(32)]
        public virtual Guid? LastUpdateUserID { get; set; }
        /// <summary>
        ///最后更新时间
        /// </summary>
        [Column("FLastUpdateTime")]
        public virtual DateTime? LastUpdateTime { get; set; }
    }

    /// <summary>
    /// 定义默认主键类型为Guid的实体基类
    /// </summary>
    public abstract class Entity : Entity<Guid>
    {

    }
}
