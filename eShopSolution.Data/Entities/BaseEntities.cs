using System;

namespace eShopSolution.Data.Entities
{
    /// <summary>
    /// Base entity: field common
    /// </summary>
    public class BaseEntities
    {
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Được tạo bởi
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Chỉnh sửa bởi
        /// </summary>
        public string UpdatedBy { get; set; }
    }
}