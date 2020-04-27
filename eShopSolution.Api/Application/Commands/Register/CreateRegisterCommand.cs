using System;
using System.ComponentModel.DataAnnotations;

namespace eShopSolution.Api.Application.Commands.Register.Create
{
    public class CreateRegisterCommand
    {
        /// <summary>
        /// Họ của người dùng
        /// </summary>
        [Display(Name ="Họ")]
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

        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        [Display(Name = "Mật khẩu")]
        public string PassWord { get; set; }

        /// <summary>
        /// Xác nhận mật khẩu
        /// </summary>
        [Display(Name = "Xác nhận mật khẩu")]
        public string ConfirmPass { get; set; }
    }
}