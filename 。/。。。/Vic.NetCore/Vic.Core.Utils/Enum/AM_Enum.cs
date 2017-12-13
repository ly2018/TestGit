using System;
using System.Collections.Generic;
using System.Text;

namespace Vic.Core.Utils.Enum
{
    public class AM_Enum
    {
        /// <summary>
        /// 菜单类型枚举
        /// </summary>
        public enum MenuTypeEnum
        {
            /// <summary>
            /// 父级菜单
            /// </summary>
            ParentMenu = 1,
            /// <summary>
            /// 子级菜单
            /// </summary>
            ChildMenu = 2,
            /// <summary>
            /// 子级按钮
            /// </summary>
            ChildButton = 3,
            /// <summary>
            /// 跳转按钮
            /// </summary>
            LinkButton = 4
        }
        /// <summary>
        /// 图片路径枚举-请勿乱上传
        /// </summary>
        public enum ImgPathEnum
        {
            /// <summary>
            /// 系统内置图标-站点相关图片
            /// </summary>
            SysImg = 1,
            /// <summary>
            ///内容相关图片-文章、bannner图等
            /// </summary>
            CmsImg = 2,
            /// <summary>
            /// 用户相关图片-头像等
            /// </summary>
            UserImg = 3,
            /// <summary>
            /// 用户身份认证图片
            /// </summary>
            IDCardImg = 4,
            /// <summary>
            /// 产品相关图片
            /// </summary>
            GoodsImg = 5,
            /// <summary>
            /// 广告相关图片
            /// </summary>
            AdvImg = 6
        }
    }
}
