using eShopSolution.Application.DtosCommon;

namespace eShopSolution.Application.Catalog.Product.Dtos.Public
{
    /// <summary>
    /// Lấy danh sách sản phẩm, phân trang base - public
    /// </summary>
    public class GetProductPagingRequest : PagingRequestBase
    {
        /// <summary>
        /// Id danh mục sản phẩm, có thể có hoặc không
        /// </summary>
        public int? CategoryId { get; set; }
    }
}