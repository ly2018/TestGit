using System;
using System.Collections.Generic;
using System.Text;

namespace Vic.Core.Utils.ResponseData
{
    public class PageData<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IList<T> Data { get; set; }
        public object TotalItem { get; set; }
    }
}