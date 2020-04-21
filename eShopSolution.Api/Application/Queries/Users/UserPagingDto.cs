using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.Api.Application.Queries.Users
{
    public class UserPagingDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Họ của người dùng
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Tên của người dùng
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Ngày sinh của người dùng
        /// </summary>
        public DateTime Dob { get; set; }

        /// <summary>
        /// ảnh của người dùng
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Địa chỉ mail của người dùng
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Địa chỉ của người dùng
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Số điện thoại của người dùng
        /// </summary>
        public string PhoneNumber { get; set; }

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
