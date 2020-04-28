using System.Collections.Generic;

namespace eShopSolution.Api.Application
{
    /// <summary>
    /// Trả về kết quả số trang và số bản ghi của 1 request
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResult<T> : PagedResultBase
    {
        /// <summary>
        /// Danh sách các item trong 1 request
        /// </summary>
        public List<T> Items { get; set; }
    }
}