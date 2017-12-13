﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Vic.Core.Utils
{
    public enum OperateResult
    {
        Success = 1,
        Fail = 2
    }
    /// <summary>
    /// 返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OperateMessage<T>
    {
        /// <summary>
        /// 操作结果
        /// </summary>
        /// <param name="result">结果</param>
        /// <param name="reload">是否重新加载</param>
        /// <param name="message">消息</param>
        /// <param name="data">返回的实体</param>
        /// <param name="recordCount">记录数</param>
        /// <param name="url">跳转Url</param>
        public OperateMessage(OperateResult result, bool reload, string message, T data, int recordCount, string url)
        {
            this.Result = result;
            this.Reload = reload;
            this.Message = message;
            this.Data = data;
            this.RecordCount = recordCount;
            this.Url = url;
        }

        public OperateMessage(string url)
            : this(OperateResult.Success, false, string.Empty, default(T), 0, url)
        {
        }

        public OperateMessage(T data, int recordCount)
            : this(OperateResult.Success, false, string.Empty, data, recordCount, string.Empty)
        {
        }

        public OperateMessage(bool reload, T data, int recordCount)
            : this(OperateResult.Success, reload, string.Empty, data, recordCount, string.Empty)
        {
        }

        public OperateMessage(OperateResult result, bool reload, string message, T data)
            : this(result, reload, message, data, 0, string.Empty)
        {
        }

        public OperateMessage(OperateResult result, bool reload, string message)
            : this(result, reload, message, default(T), 0, string.Empty)
        {
        }


        public OperateMessage(OperateResult result, bool reload, string message, int recordCount)
            : this(result, reload, message, default(T), recordCount, string.Empty)
        {
        }
        public OperateMessage(OperateResult result, bool reload, string message, int recordCount, string ids)
            : this(result, reload, message, default(T), recordCount, string.Empty)
        {
            IDs = ids;
        }

        public OperateResult Result { get; private set; }
        public bool Reload { get; private set; }
        public string Message { get; private set; }
        public T Data { get; private set; }

        public int RecordCount { get; private set; }
        public string IDs { get; private set; }

        public string Url { get; private set; }
    }
}