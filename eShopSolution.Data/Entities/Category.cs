using eShopSolution.Data.Enums;
using System.Collections.Generic;

namespace eShopSolution.Data.Entities
{
    /// <summary>
    /// Bảng danh mục sản phẩm
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Id của danh mục, tự tăng
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Thứ tự của danh mục sản phẩm
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Danh mục cha, một danh mục nằm trong danh mục khác
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Hiển thị trang chủ
        /// </summary>
        public bool IsShowHome { get; set; }

        /// <summary>
        /// Trạng thái của danh mục
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Quan hệ với bảng sản phẩm, một danh mục có nhiều sản phẩm
        /// </summary>
        public List<ProductInCategory> ProductInCategories { get; set; }

        /// <summary>
        /// Quan hệ với bảng danh mục phiên âm, thông tin của một danh mục sẽ có nhiều ngôn ngữ
        /// </summary>
        public List<CategoryTranslation> CategoryTranslations { get; set; }
    }
}