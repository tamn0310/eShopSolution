using System;

namespace eShopSolution.Data.Entities
{
    /// <summary>
    /// Hoạt động của hệ thống
    /// </summary>
    public class SystemActivity
    {
        /// <summary>
        /// Id tự tăng
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tên hành động
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// Ngày hành động
        /// </summary>
        public DateTime ActionDate { get; set; }

        /// <summary>
        /// map với bảng chức năng
        /// </summary>
        public int FunctionId { get; set; }

        /// <summary>
        /// map với thông tin người dùng
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Địa chỉ IP truy cập
        /// </summary>
        public string ClientIP { get; set; }
    }
}