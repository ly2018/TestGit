using System.Collections.Generic;
using Vic.Core.Utils.RequestParameters;

namespace Vic.Core.Utils.ResponseData
{
    /// <summary>
    /// 分页数据结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageDataResponse<T> : BaseResponse
    {
        public PageParameter QueryData { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalItem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PageHTML { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public IEnumerable<T> Data { get; set; }
    }
}
