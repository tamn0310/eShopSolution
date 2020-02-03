using eShopSolution.Data.Enums;
using System;

namespace eShopSolution.Data.Entities
{
    /// <summary>
    /// Bảng giao dịch
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Id giao dịch, tự tăng
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// Ngày giao dịch
        /// </summary>
        public DateTime TransactionDate { set; get; }

        /// <summary>
        /// Giao dịch bên ngoài
        /// </summary>
        public string ExternalTransactionId { set; get; }

        /// <summary>
        /// Tổng tiền
        /// </summary>
        public decimal Amount { set; get; }

        /// <summary>
        /// Phí ship
        /// </summary>
        public decimal Fee { set; get; }

        /// <summary>
        /// Kết quả giao dịch
        /// </summary>
        public string Result { set; get; }

        /// <summary>
        /// Lời nhắn
        /// </summary>
        public string Message { set; get; }

        /// <summary>
        /// Trạng thái giao dịch
        /// </summary>
        public TransactionStatus Status { set; get; }

        /// <summary>
        /// Nhà cung cấp
        /// </summary>
        public string Provider { set; get; }

        /// <summary>
        /// Map thông tin người dùng
        /// </summary>

        public Guid UserId { get; set; }

        /// <summary>
        /// Thông tin người dùng
        /// </summary>
        public AppUser AppUser { get; set; }
    }
}