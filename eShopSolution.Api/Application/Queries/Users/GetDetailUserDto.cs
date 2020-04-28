using System;
using System.ComponentModel.DataAnnotations;

namespace eShopSolution.Api.Application.Queries.Users
{
    public class GetDetailUserDto
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
        /// Tên đăng nhập
        /// </summary>
        [Display(Name = "Tên tài khoản")]
        public string UserName { get; set; }

        /// <summary>
        /// Ngày sinh của người dùng
        /// </summary>
        [Display(Name = "Ngày sinh")]
        public DateTime Dob { get; set; }

        /// <summary>
        /// ảnh của người dùng
        /// </summary>
        [Display(Name = "Ảnh đại diện")]
        public string Avatar { get; set; }

        /// <summary>
        /// Địa chỉ mail của người dùng
        /// </summary>
        [Display(Name = "Địa chỉ mail")]
        public string Email { get; set; }

        /// <summary>
        /// Địa chỉ của người dùng
        /// </summary>
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        /// <summary>
        /// Số điện thoại của người dùng
        /// </summary>
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        [Display(Name = "Ngày tạo")]
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