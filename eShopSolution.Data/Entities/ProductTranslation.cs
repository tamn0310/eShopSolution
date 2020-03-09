namespace eShopSolution.Data.Entities
{
    /// <summary>
    /// sản phẩm đa ngôn ngữ
    /// </summary>
    public class ProductTranslation : BaseEntities
    {
        /// <summary>
        /// Id tự tăng
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// map với bảng sản phẩm
        /// </summary>
        public int ProductId { set; get; }

        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// Mô tả của sản phẩm
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        /// Thông tin chi tiết của sản phẩm
        /// </summary>
        public string Details { set; get; }

        /// <summary>
        /// Mô tả seo của sản phẩm
        /// </summary>
        public string SeoDescription { set; get; }

        /// <summary>
        /// Tiêu để seo của sản phẩm
        /// </summary>
        public string SeoTitle { set; get; }

        /// <summary>
        /// Seo alias của sản phẩm
        /// </summary>
        public string SeoAlias { get; set; }

        /// <summary>
        /// map với thông tin bảng ngôn ngữ
        /// </summary>
        public string LanguageId { set; get; }

        /// <summary>
        /// Thông tin của sản phẩm
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Thông tin của bảng ngôn ngữ
        /// </summary>
        public Language Language { get; set; }
    }
}