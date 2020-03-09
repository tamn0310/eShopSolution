namespace eShopSolution.Data.Entities
{
    /// <summary>
    /// Quan hệ giữa sản phẩm và danh mục
    /// </summary>
    public class ProductInCategory : BaseEntities
    {
        /// <summary>
        /// map với bảng sản phẩm
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Thông tin của sản phẩm
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// map với bảng danh mục
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Thông tin của bảng danh mục
        /// </summary>
        public Category Category { get; set; }
    }
}