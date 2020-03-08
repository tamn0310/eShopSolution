using eShopSolution.Dtos.Catalog.Products;
using eShopSolution.Dtos.Catalog.Products.Public;
using eShopSolution.Dtos.Common;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Product
{
    public interface IPublicProductService
    {
        /// <summary>
        /// Lấy tất cả sản phẩm thông qua id danh mục
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Trả về 1 list sản phẩm</returns>
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetProductPagingRequest request);
    }
}