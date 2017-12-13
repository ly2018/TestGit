using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vic.Core.Domain.Entities
{
    [Table("T_CMS_Article")]
    public class Article : Entity
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Column("FSerialNumber")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SerialNumber { get; set; }
        /// <summary>
		///标题
		/// </summary>
        [Column("FTitle")]
        [MaxLength(200)]
        public string Title { get; set; }
        /// <summary>
        ///Seo搜索标题
        /// </summary>
        [Column("FSeoTitle")]
        [MaxLength(200)]
        public string SeoTitle { get; set; }
        /// <summary>
        ///文章来源
        /// </summary>
        [Column("FSource")]
        [MaxLength(200)]
        public string Source { get; set; }
        /// <summary>
        ///文章作者
        /// </summary>
        [Column("FAuthor")]
        [MaxLength(20)]
        public string Author { get; set; }
        /// <summary>
        /// 语言类型编号
        /// </summary>
        [Column("FLanguageTypeID")]
        [MaxLength(32)]
        public Guid LanguageTypeID { get; set; }
        /// <summary>
        ///类别编号
        /// </summary>
        [Column("FArticleTypeID")]
        [Required]
        public Guid ArticleTypeID { get; set; }
        /// <summary>
        ///图片
        /// </summary>
        [Column("FImages")]
        [MaxLength(200)]
        public string Images { get; set; }
        /// <summary>
        ///文字简介
        /// </summary>
        [Column("FDescription")]
        [MaxLength(800)]
        public string Description { get; set; }
        /// <summary>
        ///详细信息
        /// </summary>
        [Column("FContent", TypeName = "ntext")]
        public string Content { get; set; }
        /// <summary>
        ///浏览次数
        /// </summary>
        [Column("FBrowseNum")]
        public int BrowseNum { get; set; }
        /// <summary>
        ///备注
        /// </summary>
        [Column("FRemark")]
        [MaxLength(200)]
        public string Remark { get; set; }
        /// <summary>
        ///发表日期
        /// </summary>
        [Column("FPublishDate", TypeName = "DateTime")]
        public DateTime PublishDate { get; set; }
    }
}
