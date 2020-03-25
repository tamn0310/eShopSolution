using eShopSolution.Data.Enums;

namespace eShopSolution.Api.Application.Queries.Categories.GetAll
{
    /// <summary>
    /// Bảng danh mục sản phẩm
    /// </summary>
    public class CategoryGetAllDto
    {
        /// <summary>
        /// Id của danh mục, tự tăng
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tên danh mục
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Thứ tự của danh mục sản phẩm
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Danh mục cha, một danh mục nằm trong danh mục khác
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Hiển thị trang chủ
        /// </summary>
        public bool IsShowHome { get; set; }

        /// <summary>
        /// Trạng thái của danh mục
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Mô tả seo
        /// </summary>
        public string SeoDescription { set; get; }

        /// <summary>
        /// Tiêu đề seo
        /// </summary>
        public string SeoTitle { set; get; }

        /// <summary>
        /// Seo alias
        /// </summary>
        public string SeoAlias { get; set; }

        /// <summary>
        /// Map thông tin ngôn ngữ tương ứng
        /// </summary>
        public string LanguageId { get; set; }
    }
}