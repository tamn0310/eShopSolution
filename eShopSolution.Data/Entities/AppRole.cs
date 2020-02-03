using Microsoft.AspNetCore.Identity;
using System;

namespace eShopSolution.Data.Entities
{
    /// <summary>
    /// Bảng quản lí vai trò
    /// </summary>
    public class AppRole : IdentityRole<Guid>
    {
        /// <summary>
        /// Mô tả vai trò
        /// </summary>
        public string Description { get; set; }
    }
}