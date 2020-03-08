namespace eShopSolution.Data.Entities
{
    /// <summary>
    /// Quản lí hình ảnh của sản phẩm
    /// </summary>
    public class ProductImage : BaseEntities
    {
        /// <summary>
        /// Id hình ảnh
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// map với id sản phẩm tương ứng
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Đường dẫn ảnh
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Caption của ảnh
        /// </summary>
        public string Alt { get; set; }

        /// <summary>
        /// Hình ảnh mặc định
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Thứ tự hình ảnh
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Kích thước ảnh
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// Map thông tin bảng sản phẩm
        /// </summary>
        public Product Product { get; set; }
    }
}