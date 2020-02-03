using eShopSolution.Data.Enums;

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
    }
}