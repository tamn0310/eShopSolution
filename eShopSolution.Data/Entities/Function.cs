using eShopSolution.Data.Enums;

namespace eShopSolution.Data.Entities
{
    /// <summary>
    /// Bảng chức năng
    /// </summary>
    public class Function
    {
        /// <summary>
        /// Id tự tăng
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tên chức năng
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Danh mục cha
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Đường dấn đến chức năng
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Thứ tự
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public Status Status { get; set; }
    }
}