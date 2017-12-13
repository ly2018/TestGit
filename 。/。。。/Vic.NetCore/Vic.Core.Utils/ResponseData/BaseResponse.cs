using System.Collections.Generic;

namespace Vic.Core.Utils.ResponseData
{
    /// <summary>
    /// 数据响应父类
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 错误字段列表
        /// </summary>
        public List<ErrorField> ErrorFieldList { get; set; }
        /// <summary>
        /// 是否操作成功
        /// </summary>
        public bool IsSuccess { get; set; }

    }
}
