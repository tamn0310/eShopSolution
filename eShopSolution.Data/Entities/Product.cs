using System;
using System.Collections.Generic;

namespace eShopSolution.Data.Entities
{
    /// <summary>
    /// Sản phẩm
    /// </summary>
    public class Product : BaseEntities
    {
        /// <summary>
        /// Id của sản phẩm, tự tăng
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// giá bán của sản phẩm
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// giá nhập từ bên ngoài của sản phẩm
        /// </summary>
        public decimal OriginalPrice { get; set; }

        /// <summary>
        /// Stock
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Lượt xem của sản phẩm trên trang web
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// Quan hệ giữa sản phẩm và danh mục, một sản phẩm có nhiều danh mục
        /// </summary>
        public List<ProductInCategory> ProductInCategories { get; set; }

        /// <summary>
        /// Quan hệ với bảng chi tiết đơn hàng, một sản phẩm có nhiều chi tiết đơn hàng
        /// </summary>
        public List<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// Quan hệ với bảng giỏ hàng, một sản phẩm nằm trong giỏ hàng, giỏ hàng được lưu trong database
        /// </summary>
        public List<Cart> Carts { get; set; }

        /// <summary>
        /// Quan hệ với bảng sản phẩm phiên dịch, một thông tin sản phẩm sẽ có nhiều ngôn ngữ
        /// </summary>
        public List<ProductTranslation> ProductTranslations { get; set; }

        /// <summary>
        /// Quan hệ với bảng ProductImage, một sản phẩm có nhiều ảnh
        /// </summary>
        public List<ProductImage> ProductImages { get; set; }
    }
}