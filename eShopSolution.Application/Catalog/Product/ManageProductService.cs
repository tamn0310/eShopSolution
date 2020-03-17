using eShopSolution.Application.Common;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Dtos.Catalog.Products;
using eShopSolution.Dtos.Common;
using eShopSolution.Utilities.ExceptionCommon;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Product
{
    public class ManageProductService : IManageProductService
    {
        private readonly EShopDbContext _eShopDbContext;
        private readonly IStorageService _storageService;
        private readonly ILogger<ManageProductService> _logger;

        public ManageProductService(EShopDbContext eShopDbContext, IStorageService storageService, ILogger<ManageProductService> logger)
        {
            this._eShopDbContext = eShopDbContext;
            this._storageService = storageService;
            this._logger = logger;
        }

        public Task<int> AddImgae(int productId, List<IFormFile> files)
        {
            throw new NotImplementedException();
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
            try
            {
                var entity = new Data.Entities.Product()
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
                            Name =  request.Name,
                            Description = request.Description,
                            Details = request.Details,
                            SeoDescription = request.SeoDescription,
                            SeoAlias = request.SeoAlias,
                            SeoTitle = request.SeoTitle,
                            LanguageId = request.LanguageId
                        }
                    }
                };

                // Save image
                if (request.ThumbnailImage != null)
                {
                    entity.ProductImages = new List<ProductImage>()
                    {
                        new ProductImage()
                        {
                            Alt = "Thumbnail image",
                            CreatedDate = DateTime.Now,
                            FileSize = request.ThumbnailImage.Length,
                            Url = await this.SaveFile(request.ThumbnailImage),
                            IsDefault = true,
                            SortOrder = 1
                        }
                    };
                }
                _eShopDbContext.Products.Add(entity);
                await _eShopDbContext.SaveChangesAsync();
                return entity.Id;
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, e);
                throw;
            }
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

            var images = _eShopDbContext.ProductImages.Where(x => x.ProductId == productId);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.Url);
            }

            _eShopDbContext.Products.Remove(product);
            return await _eShopDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Lấy tất cả sản phẩm, phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
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
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
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

        /// <summary>
        /// Lấy ra thông tin chi tiết sản phẩm theo id truyền vào
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<ProductViewModel> GetById(int productId, string languageId)
        {
            var product = await _eShopDbContext.Products.FindAsync(productId);
            var productTranslation = await _eShopDbContext.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == productId
            && x.LanguageId == languageId);

            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                DateCreated = product.CreatedDate,
                Description = productTranslation != null ? productTranslation.Description : null,
                LanguageId = productTranslation.LanguageId,
                Details = productTranslation != null ? productTranslation.Details : null,
                Name = productTranslation != null ? productTranslation.Name : null,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                SeoAlias = productTranslation != null ? productTranslation.SeoAlias : null,
                SeoDescription = productTranslation != null ? productTranslation.SeoDescription : null,
                SeoTitle = productTranslation != null ? productTranslation.SeoTitle : null,
                Stock = product.Stock,
                ViewCount = product.ViewCount
            };
            return productViewModel;
        }

        public Task<int> RemoveImage(int imageId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cập nhật thông tin sản phẩm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await this._eShopDbContext.Products.FindAsync(request.Id);
            var productTranslation = await this._eShopDbContext.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == request.Id && x.LanguageId == request.LanguageId);
            if (product == null || productTranslation == null)
            {
                throw new EShopException($"Cannot find a product with id {request.Id}");
            }

            productTranslation.Name = request.Name;
            productTranslation.Details = request.Details;
            productTranslation.Description = request.Description;
            productTranslation.SeoAlias = request.SeoAlias;
            productTranslation.SeoDescription = request.SeoDescription;
            productTranslation.SeoTitle = request.SeoTitle;

            // Update Image
            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = await _eShopDbContext.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.ProductId == request.Id);
                if (thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.ThumbnailImage.Length;
                    thumbnailImage.Url = await this.SaveFile(request.ThumbnailImage);
                    thumbnailImage.UpdatedDate = DateTime.Now;
                    _eShopDbContext.ProductImages.Update(thumbnailImage);
                }
            }

            return await this._eShopDbContext.SaveChangesAsync();
        }

        public Task<int> UpdateImage(int imageId, string Alt, bool IsDefault)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cập nhật giá tiền sản phẩm
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="newPrice"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePrice(UpdateProductPriceCommand command)
        {
            var product = await this._eShopDbContext.Products.FindAsync(command.Id);
            if (product == null)
            {
                throw new EShopException($"Cannot find a product with id {command.Id}");
            }

            product.UpdatedDate = DateTime.Now;
            product.Price = command.Price;
            return await this._eShopDbContext.SaveChangesAsync() > 0; // return > 0 => true, return < 0 => false
        }

        /// <summary>
        /// Cập nhật lại số lượng sản phẩm
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="addedQuantity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateStock(UpdateStockProductCommand command)
        {
            var product = await this._eShopDbContext.Products.FindAsync(command.Id);
            if (product == null)
            {
                throw new EShopException($"Cannot find a product with id {command.Id}");
            }
            product.UpdatedDate = DateTime.Now;
            product.Stock += command.Quantity;
            return await this._eShopDbContext.SaveChangesAsync() > 0;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var root = "wwwroot";
            var path = Path.Combine(root, "user-content");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}