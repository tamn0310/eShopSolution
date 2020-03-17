using eShopSolution.Dtos.Common;

namespace eShopSolution.Dtos.Catalog.Products
{
    /// <summary>
    /// Lấy danh sách sản phẩm, phân trang base - public
    /// </summary>
    public class GetPublicProductPagingRequest : PagingRequestBase
    {
        /// <summary>
        /// id cua ngon ngu
        /// </summary>
        public string LanguageId { get; set; }

        /// <summary>
        /// Id danh mục sản phẩm, có thể có hoặc không
        /// </summary>
        public int? CategoryId { get; set; }
    }
}