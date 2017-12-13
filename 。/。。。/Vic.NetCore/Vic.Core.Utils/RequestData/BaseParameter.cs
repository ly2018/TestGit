using System;
namespace Vic.Core.Utils.RequestParameters
{
    /// <summary>
    /// 请求参数父类，所有请求实体需要继承该类
    /// </summary>
    public class BaseParameter
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 服务器返回客户端数据格式，当前只支持Json
        /// 通过反序列化转换为查询对应参数
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public int Status { get; set; }
    }
}
