﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace eShopSolution.Data.Entities
{
    /// <summary>
    /// Thông tin người dùng
    /// </summary>
    public class AppUser : IdentityUser<Guid>
    {
        /// <summary>
        /// Họ của người dùng
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Tên của người dùng
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Ngày sinh của người dùng
        /// </summary>
        public DateTime Dob { get; set; }

        /// <summary>
        /// Địa chỉ của người dùng
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// ảnh đại diện
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Thông tin giỏ hàng của người dùng
        /// </summary>
        public List<Cart> Carts { get; set; }

        /// <summary>
        /// Thông tin đơn hàng của người dung
        /// </summary>
        public List<Order> Orders { get; set; }

        /// <summary>
        /// Thông tin giao dịch của người dùng
        /// </summary>
        public List<Transaction> Transactions { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Tạo bởi
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Cập nhật bởi
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}