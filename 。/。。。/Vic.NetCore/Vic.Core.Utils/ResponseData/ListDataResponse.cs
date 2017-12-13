using System.Collections.Generic;

namespace Vic.Core.Utils.ResponseData
{
    /// <summary>
    /// 客户端返回集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListDataResponse<T>:BaseResponse
    {
        public IList<T> Data { get; set; }
    }
}
