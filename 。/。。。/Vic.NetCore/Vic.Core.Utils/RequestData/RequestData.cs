namespace Vic.Core.Utils.RequestParameters
{
    /// <summary>
    /// 请求数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RequestData<T> : BaseRequestData
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }
}
