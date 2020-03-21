using System;

namespace eShopSolution.Dtos.User
{
    public class RegisterCommandDto
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
        /// Số điện thoại người dùng
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Địa chỉ email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// Xác nhận mật khẩu
        /// </summary>
        public string ConfirmPass { get; set; }
    }
}