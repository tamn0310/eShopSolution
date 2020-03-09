using eShopSolution.Data.Enums;
using System;

namespace eShopSolution.Data.Entities
{
    /// <summary>
    /// Bảng khuyến mãi của shop
    /// </summary>
    public class Promotion : BaseEntities
    {
        /// <summary>
        /// Id tự tăng
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// Khuyến mãi áp dụng từ ngày
        /// </summary>
        public DateTime FromDate { set; get; }

        /// <summary>
        /// Ngày kết thúc khuyến mãi
        /// </summary>
        public DateTime ToDate { set; get; }

        /// <summary>
        /// áp dụng cho tất cả
        /// </summary>
        public bool ApplyForAll { set; get; }

        /// <summary>
        /// Phẩn trăm giảm giá
        /// </summary>
        public int? DiscountPercent { set; get; }

        /// <summary>
        /// Số tiền chiếc khấu
        /// </summary>
        public decimal? DiscountAmount { set; get; }

        /// <summary>
        /// các sản phẩm giảm giá
        /// </summary>
        public string ProductIds { set; get; }

        /// <summary>
        /// Các danh mục sản phẩm giảm giá
        /// </summary>
        public string ProductCategoryIds { set; get; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public Status Status { set; get; }

        /// <summary>
        /// Tên sự kiện giảm giá
        /// </summary>
        public string Name { set; get; }
    }
}