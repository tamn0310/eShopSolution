namespace eShopSolution.Application.Catalog.Product.Dtos.Manage
{
    /// <summary>
    /// Yêu cầu tạo mới sản phẩm - admin
    /// </summary>
    public class ProductCreateRequest
    {
        /// <summary>
        /// Giá bán
        /// </summary>
        public decimal Price { set; get; }

        /// <summary>
        /// Giá gốc
        /// </summary>
        public decimal OriginalPrice { set; get; }

        /// <summary>
        /// Tình trạng
        /// </summary>
        public int Stock { set; get; }

        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// Mô tả sản phẩm
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        /// Thông tin chi tiết sản phẩm
        /// </summary>
        public string Details { set; get; }

        /// <summary>
        /// Mô tả seo
        /// </summary>
        public string SeoDescription { set; get; }

        /// <summary>
        /// Tiêu đề seo
        /// </summary>
        public string SeoTitle { set; get; }

        /// <summary>
        /// Seo alias
        /// </summary>
        public string SeoAlias { get; set; }

        /// <summary>
        /// Map thông tin ngôn ngữ
        /// </summary>
        public string LanguageId { set; get; }
    }
}