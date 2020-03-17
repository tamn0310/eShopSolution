using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Dtos.Catalog.Categories.Commands.Create;
using eShopSolution.Dtos.Catalog.Categories.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace eShopSolution.Application.Catalog.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly EShopDbContext _eShopDbContext;

        public CategoryService(EShopDbContext eShopDbContext)
        {
            this._eShopDbContext = eShopDbContext;
        }

        /// <summary>
        /// Tạo mới danh mục sản phẩm
        /// </summary>
        /// <param name="commmand"></param>
        /// <returns></returns>
        public async Task<int> Create(CategoryCreateCommand commmand)
        {
            var entity = new Data.Entities.Category()
            {
                SortOrder = commmand.SortOrder,
                ParentId = commmand.ParentId,
                IsShowHome = commmand.IsShowHome,
                Status = commmand.Status,
                CreatedDate = DateTime.Now,
                CategoryTranslations = new List<CategoryTranslation>()
                {
                    new CategoryTranslation()
                    {
                        Name = commmand.Name,
                        SeoAlias = commmand.SeoAlias,
                        SeoDescription = commmand.SeoDescription,
                        SeoTitle = commmand.SeoTitle,
                        LanguageId = commmand.LanguageId
                    }
                }
            };

            this._eShopDbContext.Categories.Add(entity);
            return await _eShopDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Lấy tất cả danh mục sản phẩm
        /// </summary>
        /// <param name="languageId"></param>
        /// <returns></returns>
        public async Task<List<CategoryQueryDto>> GetAll(string languageId)
        {
            var query = from c in _eShopDbContext.Categories
                        join ct in _eShopDbContext.ProductTranslations on c.Id equals ct.ProductId
                        join pic in _eShopDbContext.ProductInCategories on c.Id equals pic.ProductId
                        where ct.LanguageId == languageId
                        select new { c, ct, pic };

            var data = await query.Select(x => new CategoryQueryDto()
            {
                Id = x.c.Id,
                Name = x.ct.Name,
                SortOrder = x.c.SortOrder,
                ParentId = x.c.ParentId,
                IsShowHome = x.c.IsShowHome,
                Status = x.c.Status,
                SeoAlias = x.ct.SeoAlias,
                SeoDescription = x.ct.SeoDescription,
                SeoTitle = x.ct.SeoTitle,
                LanguageId = x.ct.LanguageId
            }).ToListAsync();
            return data;
        }

        /// <summary>
        /// Lấy ra chi tiết danh mục sản phẩm theo id và languageId truyền vào
        /// </summary>
        /// <param name="languageId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CategoryQueryDto> GetById(int categoryId, string languageId)
        {
            var category = await _eShopDbContext.Categories.FindAsync(categoryId);
            var categoryTranslation = await _eShopDbContext.CategoryTranslations.FirstOrDefaultAsync(x => x.CategoryId == categoryId && x.LanguageId == languageId);

            var categoryViewModel = new CategoryQueryDto()
            {
                Id = category.Id,
                Name = categoryTranslation != null ? categoryTranslation.Name : null,
                SortOrder = category.SortOrder,
                ParentId = category.ParentId,
                IsShowHome = category.IsShowHome,
                Status = category.Status,
                SeoAlias = categoryTranslation != null ? categoryTranslation.SeoAlias : null,
                SeoDescription = categoryTranslation != null ? categoryTranslation.SeoDescription : null,
                SeoTitle = categoryTranslation != null ? categoryTranslation.SeoTitle : null,
                LanguageId = categoryTranslation.LanguageId
            };
            return categoryViewModel;
        }
    }
}