using eShopSolution.Data.Enums;
using System;
using System.Collections.Generic;

namespace eShopSolution.Data.Entities
{
    /// <summary>
    /// Bảng đơn hàng
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Id của đơn hàng, tự tăng
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ngày tạo đơn hàng
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Map với thông tin người dùng
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Tên của người đặt hàng
        /// </summary>
        public string ShipName { get; set; }

        /// <summary>
        /// Số điện thoại của người đặt hàng
        /// </summary>
        public string ShipPhone { get; set; }

        /// <summary>
        /// Địa chỉ mail của người đặt hàng
        /// </summary>
        public string ShipEmail { get; set; }

        /// <summary>
        /// Địa chỉ của người đặt hàng
        /// </summary>
        public string ShipAddress { get; set; }

        /// <summary>
        /// Trạng thái đơn hàng
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// map với bảng chi tiết đơn hàng, một đơn hàng có nhiều chi tiết đơn hàng
        /// </summary>
        public List<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// Thông tin của người dùng
        /// </summary>
        public AppUser AppUser { get; set; }
    }
}