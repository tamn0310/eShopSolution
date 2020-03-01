using eShopSolution.Application.Catalog.Product.Dtos;
using eShopSolution.Application.Catalog.Product.Dtos.Manage;
using eShopSolution.Application.DtosCommon;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.ExceptionCommon;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Product
{
    public class ManageProductService : IManageProductService
    {
        private readonly EShopDbContext _eShopDbContext;

        public ManageProductService(EShopDbContext eShopDbContext)
        {
            this._eShopDbContext = eShopDbContext;
        }

        /// <summary>
        /// Thêm lượt xem sản phẩm khi truy cập vào sản phẩm
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task AddViewcount(int productId)
        {
            var product = await _eShopDbContext.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _eShopDbContext.SaveChangesAsync();
        }

        /// <summary>
        ///  Thêm mới sản phẩm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Data.Entities.Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                CreatedDate = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        CreatedDate = DateTime.Now,
                        LanguageId = request.LanguageId
                    }
                }
            };
            _eShopDbContext.Products.Add(product);
            return await _eShopDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Xóa thông tin sản phẩm thông qua id sản phẩm
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<int> Delete(int productId)
        {
            var product = await _eShopDbContext.Products.FindAsync(productId);
            if (product == null)
            {
                throw new EShopException($"Cannot find a product with productId {productId}");
            }
            _eShopDbContext.Products.Remove(product);
            return await _eShopDbContext.SaveChangesAsync();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request)
        {
            /*Select join data*/
            var query = from p in _eShopDbContext.Products
                        join pt in _eShopDbContext.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _eShopDbContext.ProductInCategories on p.Id equals pic.ProductId
                        join c in _eShopDbContext.Categories on pic.CategoryId equals c.Id
                        where pt.Name.Contains(request.Keyword)
                        select new { p, pt, pic };

            /*Filter data*/
            // Kiểm tra có tồn tại param keyword truyền vào
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));
            }

            // Kiểm tra có tồn tại param CategoryIds truyền vào
            if (request.CategoryIds.Count > 0)
            {
                query = query.Where(x => request.CategoryIds.Contains(x.pic.CategoryId));
            }

            /*Paging*/
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.pageIndex - 1) * request.pageSize)
                .Take(request.pageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.CreatedDate,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    SeoDescription = x.pt.SeoDescription,
                    SeoAlias = x.pt.SeoAlias,
                    SeoTitle = x.pt.SeoTitle,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();

            /*Select and projection*/
            var pageResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };

            return pageResult;
        }

        public Task<int> Update(ProductUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            throw new NotImplementedException();
        }
    }
}