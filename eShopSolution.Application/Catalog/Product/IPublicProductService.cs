using eShopSolution.Application.Catalog.Product.Dtos;
using eShopSolution.Application.Catalog.Product.Dtos.Public;
using eShopSolution.Application.DtosCommon;
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