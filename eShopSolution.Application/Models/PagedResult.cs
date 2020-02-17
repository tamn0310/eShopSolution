using System.Collections.Generic;

namespace eShopSolution.Application.Models
{
    /// <summary>
    /// Dùng để đếm bản ghi, phân trang
    /// </summary>
    public class PagedResult<T>
    {
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Danh sách các items có trong bản ghi
        /// </summary>
        public List<T> Items { get; set; }
    }
}