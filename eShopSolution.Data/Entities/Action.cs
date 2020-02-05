using eShopSolution.Data.Enums;
using System.Collections.Generic;

namespace eShopSolution.Data.Entities
{
    /// <summary>
    /// Bảng thể hiện các hành động
    /// </summary>
    public class Action
    {
        /// <summary>
        /// Id tự tăng
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tên hành động
        /// </summary>
        public ActionName Name { get; set; }

        /// <summary>
        /// Map thông tin bảng permission
        /// </summary>
        public List<Permission> Permission { get; set; }
    }
}