using eShopSolution.Data.Enums;

namespace eShopSolution.Data.Entities
{
    /// <summary>
    /// Liên hệ
    /// </summary>
    public class Contact : BaseEntities
    {
        /// <summary>
        /// Id tự tăng
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// Tên
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// Email của shop
        /// </summary>
        public string Email { set; get; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { set; get; }

        /// <summary>
        /// Thông điệp
        /// </summary>
        public string Message { set; get; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public Status Status { set; get; }
    }
}