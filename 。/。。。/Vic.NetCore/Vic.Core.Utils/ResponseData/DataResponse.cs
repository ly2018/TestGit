namespace Vic.Core.Utils.ResponseData
{
    /// <summary>
    /// 单条数据响应
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataResponse<T>:BaseResponse
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }       
    }
}
