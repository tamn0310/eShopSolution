using System;

namespace eShopSolution.Data.Entities
{
    /// <summary>
    /// Bảng giỏ hàng
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Id của giỏ hàng, tự tăng
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// map với id của bảng sản phẩm
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Số lượng của sản phẩm
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gía tiền của sản phẩm
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// map với id của người dùng
        /// </summary>

        public Guid UserId { get; set; }
        /// <summary>
        /// map với bảng sản phẩm
        /// </summary>

        public Product Product { get; set; }
        /// <summary>
        /// Ngày cho hàng vảo giỏ
        /// </summary>

        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Thông tin của người dùng
        /// </summary>
        public AppUser AppUser { get; set; }
    }
}