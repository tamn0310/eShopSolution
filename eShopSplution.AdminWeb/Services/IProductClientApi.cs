using eShopSolution.Api.Application;
using eShopSolution.Api.Application.Queries.Products;
using eShopSolution.Application.Queries.Products;
using System.Threading.Tasks;

namespace eShopSplution.AdminWeb.Services
{
    public interface IProductClientApi
    {
        /// <summary>
        /// Lấy ra tất cả sản phẩm, phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedResult<ProductViewModel>> GetAll(GetManageProductPagingRequest request);
    }
}