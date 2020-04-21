using eShopSolution.Api.Application;
using eShopSolution.Api.Application.Commands.ProductImages;
using eShopSolution.Api.Application.Commands.ProductImages.Create;
using eShopSolution.Api.Application.Commands.ProductImages.Update;
using eShopSolution.Api.Application.Commands.Products.Create;
using eShopSolution.Api.Application.Commands.Products.Update;
using eShopSolution.Api.Application.Commands.Produts.Update;
using eShopSolution.Api.Application.Handler.Catalog.Product;
using eShopSolution.Api.Application.Handler.Common;
using eShopSolution.Api.Application.Queries.Products;
using eShopSolution.Application.Queries.Products;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.ExceptionCommon;
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
    public class ManageProductHandler : IManageProductHandler
    {
        private readonly EShopDbContext _eShopDbContext;
        private readonly IStorageService _storageService;
        private readonly ILogger<ManageProductHandler> _logger;

        public ManageProductHandler(EShopDbContext eShopDbContext, IStorageService storageService, ILogger<ManageProductHandler> logger)
        {
            this._eShopDbContext = eShopDbContext;
            this._storageService = storageService;
            this._logger = logger;
        }

        public async Task<int> AddImage(int productId, CreateProductImageCommand command)
        {
            var image = new ProductImage()
            {
                Alt = command.Alt,
                CreatedDate = DateTime.Now,
                IsDefault = command.IsDefault,
                SortOrder = command.SortOrder,
                ProductId = productId
            };

            if (command.ImageFile != null)
            {
                image.Url = await this.SaveFile(command.ImageFile);
                image.FileSize = command.ImageFile.Length;
            }
            _eShopDbContext.ProductImages.Add(image);
            await _eShopDbContext.SaveChangesAsync();
            return image.Id;
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
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<int> Create(CreateProductCommand command)
        {
            try
            {
                var entity = new Data.Entities.Product()
                {
                    Price = command.Price,
                    OriginalPrice = command.OriginalPrice,
                    Stock = command.Stock,
                    ViewCount = 0,
                    CreatedDate = DateTime.Now,
                    ProductTranslations = new List<ProductTranslation>()
                    {
                        new ProductTranslation()
                        {
                            Name =  command.Name,
                            Description = command.Description,
                            Details = command.Details,
                            SeoDescription = command.SeoDescription,
                            SeoAlias = command.SeoAlias,
                            SeoTitle = command.SeoTitle,
                            LanguageId = command.LanguageId
                        }
                    }
                };

                // Save image
                if (command.ThumbnailImage != null)
                {
                    entity.ProductImages = new List<ProductImage>()
                    {
                        new ProductImage()
                        {
                            Alt = "Thumbnail image",
                            CreatedDate = DateTime.Now,
                            FileSize = command.ThumbnailImage.Length,
                            Url = await this.SaveFile(command.ThumbnailImage),
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

        public async Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request)
        {
            //1. Select join
            var query = from p in _eShopDbContext.Products
                        join pt in _eShopDbContext.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _eShopDbContext.ProductInCategories on p.Id equals pic.ProductId
                        join c in _eShopDbContext.Categories on pic.CategoryId equals c.Id
                        where pt.LanguageId == request.LanguageId
                        select new { p, pt, pic };
            //2. filter
            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
            {
                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
            }
            //3. Paging
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
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pagedResult;
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
        /// <param name="languageId"></param>
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

        /// <summary>
        /// Lấy ra thông tin chi tiết ảnh thông qua id truyền vào
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="imageId"></param>
        /// <returns></returns>
        public async Task<ProductImageViewModel> GetImageById(int productId, int imageId)
        {
            var query = await _eShopDbContext.ProductImages.FindAsync(imageId);
            if (query == null)
            {
                throw new EShopException($"Cannot find an image with id {imageId}");
            }

            var viewModel = new ProductImageViewModel()
            {
                Id = query.Id,
                Alt = query.Alt,
                CreatedDate = query.CreatedDate,
                FileSize = query.FileSize,
                Url = query.Url,
                SortOrder = query.SortOrder,
                ProductId = query.ProductId
            };

            return viewModel;
        }

        /// <summary>
        /// Lấy ra tất cả thuộc sản phẩm thông qua id sản phẩm truyền vào
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<List<ProductImageViewModel>> GetListImage(int productId)
        {
            var query = _eShopDbContext.ProductImages.Where(x => x.ProductId == productId).Select(i => new ProductImageViewModel()
            {
                Id = i.Id,
                Alt = i.Alt,
                CreatedDate = i.CreatedDate,
                FileSize = i.FileSize,
                Url = i.Url,
                SortOrder = i.SortOrder,
                ProductId = i.ProductId
            }).ToListAsync();

            return await query;
        }

        /// <summary>
        /// Xóa ảnh theo id
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        public async Task<int> RemoveImage(int imageId)
        {
            var productImage = await _eShopDbContext.ProductImages.FindAsync(imageId);
            if (productImage == null)
            {
                throw new EShopException($"Cannot find an image with id {imageId}");
            }

            _eShopDbContext.ProductImages.Remove(productImage);
            return await _eShopDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Cập nhật thông tin sản phẩm
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<int> Update(UpdateProductCommand command)
        {
            var product = await this._eShopDbContext.Products.FindAsync(command.Id);
            var productTranslation = await this._eShopDbContext.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == command.Id && x.LanguageId == command.LanguageId);
            if (product == null || productTranslation == null)
            {
                throw new EShopException($"Cannot find a product with id {command.Id}");
            }

            productTranslation.Name = command.Name;
            productTranslation.Details = command.Details;
            productTranslation.Description = command.Description;
            productTranslation.SeoAlias = command.SeoAlias;
            productTranslation.SeoDescription = command.SeoDescription;
            productTranslation.SeoTitle = command.SeoTitle;

            // Update Image
            if (command.ThumbnailImage != null)
            {
                var thumbnailImage = await _eShopDbContext.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.ProductId == command.Id);
                if (thumbnailImage != null)
                {
                    thumbnailImage.FileSize = command.ThumbnailImage.Length;
                    thumbnailImage.Url = await this.SaveFile(command.ThumbnailImage);
                    thumbnailImage.UpdatedDate = DateTime.Now;
                    _eShopDbContext.ProductImages.Update(thumbnailImage);
                }
            }

            return await this._eShopDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Cập nhật lại hình ảnh sản phẩm
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<int> UpdateImage(int imageId, UpdateProductImageCommand command)
        {
            var productImage = await _eShopDbContext.ProductImages.FindAsync(imageId);
            if (productImage == null)
            {
                throw new EShopException($"Cannot find an image with id {imageId}");
            }

            if (command.ImageFile != null)
            {
                productImage.Url = await this.SaveFile(command.ImageFile);
                productImage.FileSize = command.ImageFile.Length;
            }
            _eShopDbContext.ProductImages.Update(productImage);
            return await _eShopDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Cập nhật giá tiền sản phẩm
        /// </summary>
        /// <param name="command"></param>
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
        /// <param name="command"></param>
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