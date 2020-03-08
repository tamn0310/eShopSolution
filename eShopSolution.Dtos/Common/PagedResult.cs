using System.Collections.Generic;

namespace eShopSolution.Dtos.Common
{
    /// <summary>
    /// Trả về kết quả số trang và số bản ghi của 1 request
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// Danh sách các item trong 1 request
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// Tổng số bản ghi có trong lần request đó
        /// </summary>
        public int TotalRecord { get; set; }
    }
}