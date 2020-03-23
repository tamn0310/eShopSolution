using System;

namespace eShopSolution.Dtos.User
{
    public class LoginCommandDto
    {
        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Mật khẩu đăng nhập
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// Nhớ đăng nhập
        /// </summary>
        public Boolean RememberMe { get; set; }
    }
}