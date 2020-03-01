using eShopSolution.Application.Catalog.Product.Dtos;
using eShopSolution.Application.Catalog.Product.Dtos.Manage;
using eShopSolution.Application.DtosCommon;
using eShopSolution.Data.EF;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Product
{
    public class PublicProductService : IPublicProductService
    {
        private readonly EShopDbContext _eShopDbContext;

        public PublicProductService(EShopDbContext eShopDbContext)
        {
            this._eShopDbContext = eShopDbContext;
        }

        public Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetProductPagingRequest request)
        {
        }
    }
}