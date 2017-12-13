namespace Vic.Core.Utils.RequestParameters
{
    /// <summary>
    /// 分页请求参数
    /// </summary>
    public class PageParameter : BaseParameter
    {
        public PageParameter()
        {
            if (this.CurPage <= 0)
            {
                this.CurPage = 1;
            }
            if (this.PageSize <= 0 || this.PageSize >= 20)
            {
                this.PageSize = 10;
            }
        }
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页,从1开始
        /// </summary>
        public int CurPage { get; set; }
    }
}
