using System.Collections.Generic;

namespace eShopSolution.Data.Entities
{
    /// <summary>
    /// Bảng đa ngôn ngữ
    /// </summary>
    public class Language
    {
        /// <summary>
        /// Id của ngôn ngữ
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Tên của ngôn ngữ
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ngôn ngữ mặc định
        /// </summary>
        public bool IsDefaults { get; set; }

        /// <summary>
        /// Thông tin của bảng sản phẩm đa ngôn ngữ
        /// </summary>
        public List<ProductTranslation> ProductTranslations { get; set; }

        /// <summary>
        /// Thông tin của bảng danh mục đa ngôn ngữ
        /// </summary>
        public List<CategoryTranslation> CategoryTranslations { get; set; }
    }
}