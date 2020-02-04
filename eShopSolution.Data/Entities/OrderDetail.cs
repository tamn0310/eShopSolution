namespace eShopSolution.Data.Entities
{
    /// <summary>
    /// Bảng chi tiết đơn hàng
    /// </summary>
    public class OrderDetail
    {
        /// <summary>
        /// map với bảng đơn hàng
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// map với bảng sản phẩm
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Số lượng sản phẩm
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// giá tiền của sản phẩm
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Thông tin của đơn hàng, một chi tiết có một đơn hàng
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// Thông tin của sản phẩm
        /// </summary>
        public Product Product { get; set; }
    }
}