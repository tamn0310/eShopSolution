using System;

namespace eShopSolution.Data.Entities
{
    /// <summary>
    /// Bảng quyền
    /// </summary>
    public class Permission : BaseEntities
    {
        /// <summary>
        /// Id tự tăng
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// map với bảng AppRole
        /// </summary>
        public Guid RodeId { get; set; }

        /// <summary>
        /// map với bảng Action
        /// </summary>
        public int ActionId { get; set; }

        /// <summary>
        /// map với bảng Funtion
        /// </summary>
        public int FunctionId { get; set; }

        /// <summary>
        /// Thông tin bảng role
        /// </summary>
        public AppRole AppRole { get; set; }

        /// <summary>
        /// Thông tin bảng action
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        /// thông tin bảng function
        /// </summary>
        public Function Function { get; set; }
    }
}