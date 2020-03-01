namespace eShopSolution.Application.Catalog.Product.Dtos.Manage
{
    /// <summary>
    /// Yêu cầu cập nhật sản phẩm - admin
    /// </summary>
    public class ProductUpdateRequest
    {
        /// <summary>
        /// Id sản phẩm, cập nhật
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tên sản phẩm, cập nhật
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// Mô tả sản phầm, cập nhật
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        /// Chi tiết sản phẩm, cập nhật
        /// </summary>
        public string Details { set; get; }

        /// <summary>
        /// Mô tả seo, cập nhật
        /// </summary>
        public string SeoDescription { set; get; }

        /// <summary>
        /// Tiêu đề seo, cập nhật
        /// </summary>
        public string SeoTitle { set; get; }

        /// <summary>
        /// seo alias, cập nhật
        /// </summary>
        public string SeoAlias { get; set; }

        /// <summary>
        /// Map thông tin ngôn ngữ, cập nhật
        /// </summary>
        public string LanguageId { set; get; }
    }
}