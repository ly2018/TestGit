using System;
using System.Collections.Generic;
using System.Text;

namespace Vic.Core.Utils.Models
{
    public class Right_Privilege
    {
        /// <summary>
        /// 分类名
        /// </summary>
        public string cateName { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public Guid cateId { get; set; }

        /// <summary>
        /// 父级分类ID
        /// </summary>
        public Guid catePid { get; set; }

        /// <summary>
        /// 子集分类
        /// </summary>
        public List<Right_Privilege> childCateList { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public List<Right_Control> cateQuanxian { get; set; }
    }

    public class Right_Control
    {
        //权限名
        public string quanxian { get; set; }

        //权限ID
        public Guid quanxianid { get; set; }

        public int state { set; get; }
    }
}
