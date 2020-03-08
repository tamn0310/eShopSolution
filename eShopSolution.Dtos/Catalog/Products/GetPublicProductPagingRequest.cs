using eShopSolution.Dtos.Common;

namespace eShopSolution.Dtos.Catalog.Products.Public
{
    /// <summary>
    /// Lấy danh sách sản phẩm, phân trang base - public
    /// </summary>
    public class GetPublicProductPagingRequest : PagingRequestBase
    {
        /// <summary>
        /// Id danh mục sản phẩm, có thể có hoặc không
        /// </summary>
        public int? CategoryId { get; set; }
    }
}