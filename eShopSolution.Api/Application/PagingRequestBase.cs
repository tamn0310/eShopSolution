namespace eShopSolution.Api.Application
{
    /// <summary>
    /// phân trang
    /// </summary>
    public class PagingRequestBase : RequestBase
    {
        /// <summary>
        /// Trang đầu
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}