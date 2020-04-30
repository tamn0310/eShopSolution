using System;

namespace eShopSolution.Api.Application.Commands.User
{
    /// <summary>
    /// Dto: xóa người dùng
    /// </summary>
    public class DeleteUserCommand
    {
        /// <summary>
        /// Id người dùng
        /// </summary>
        public Guid Id { get; set; }
    }
}