using eShopSolution.Api.Application.Queries.Products;
using eShopSolution.Dtos.Catalog.Products;
using System.Threading.Tasks;

namespace eShopSolution.Api.Application.Handler.Catalog.Product
{
    public interface IPublicProductHandler
    {
        /// <summary>
        /// Lấy tất cả sản phẩm thông qua id danh mục
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Trả về 1 list sản phẩm</returns>
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);
    }
}