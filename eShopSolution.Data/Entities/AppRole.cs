using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

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

        /// <summary>
        /// Map thông tin bảng permission
        /// </summary>
        public List<Permission> Permission { get; set; }
    }
}