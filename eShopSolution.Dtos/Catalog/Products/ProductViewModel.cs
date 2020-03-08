using System;

namespace eShopSolution.Dtos.Catalog.Products
{
    /// <summary>
    /// Dto: sản phẩm
    /// </summary>
    public class ProductViewModel
    {
        /// <summary>
        /// Id sản phẩm
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// Giá bán
        /// </summary>
        public decimal Price { set; get; }

        /// <summary>
        /// Giá gốc
        /// </summary>
        public decimal OriginalPrice { set; get; }

        /// <summary>
        /// Tình trạng sản phẩm
        /// </summary>
        public int Stock { set; get; }

        /// <summary>
        /// Lượt xem
        /// </summary>
        public int ViewCount { set; get; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime DateCreated { set; get; }

        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        /// Thông tin chi tiết
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
        /// Map thông tin ngôn ngữ tương ứng
        /// </summary>
        public string LanguageId { set; get; }
    }
}