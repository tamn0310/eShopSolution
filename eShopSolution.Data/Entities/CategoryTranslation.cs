namespace eShopSolution.Data.Entities
{
    /// <summary>
    /// Danh mục đa ngôn ngữ
    /// </summary>
    public class CategoryTranslation : BaseEntities
    {
        /// <summary>
        /// Id tự tăng
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// map với bảng danh mục
        /// </summary>
        public int CategoryId { set; get; }

        /// <summary>
        /// Tên danh mục sản phẩm
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// Mô tả seo của danh mục
        /// </summary>
        public string SeoDescription { set; get; }

        /// <summary>
        /// Tiêu để seo của danh mục
        /// </summary>
        public string SeoTitle { set; get; }

        /// <summary>
        /// Seo alias của danh mục sản phẩm
        /// </summary>
        public string SeoAlias { set; get; }

        /// <summary>
        /// map với bảng ngôn ngữ
        /// </summary>
        public string LanguageId { set; get; }

        /// <summary>
        /// Thông tin của bảng danh mục
        /// </summary>

        public Category Category { get; set; }

        /// <summary>
        /// Thông tin của bảng ngôn ngữ
        /// </summary>
        public Language Language { get; set; }
    }
}