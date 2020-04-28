using System;
using System.ComponentModel.DataAnnotations;

namespace eShopSolution.Api.Application.Commands.User
{
    /// <summary>
    /// Cập nhật người dùng
    /// </summary>
    public class UpdateUserCommand
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Họ của người dùng
        /// </summary>
        [Display(Name = "Họ")]
        public string FirstName { get; set; }

        /// <summary>
        /// Tên của người dùng
        /// </summary>
        [Display(Name = "Tên")]
        public string LastName { get; set; }

        /// <summary>
        /// Ngày sinh của người dùng
        /// </summary>
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        /// <summary>
        /// Số điện thoại người dùng
        /// </summary>
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Địa chỉ email
        /// </summary>
        [Display(Name = "Địa chỉ mail")]
        public string Email { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
    }
}