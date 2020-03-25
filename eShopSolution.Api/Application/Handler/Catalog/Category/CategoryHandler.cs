using eShopSolution.Api.Application.Commands.Categories.Create;
using eShopSolution.Api.Application.Queries.Categories.GetAll;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.Api.Application.Handler.Catalog.Category
{
    public class CategoryHandler : ICategoryHandler
    {
        private readonly EShopDbContext _eShopDbContext;

        public CategoryHandler(EShopDbContext eShopDbContext)
        {
            this._eShopDbContext = eShopDbContext;
        }

        /// <summary>
        /// Tạo mới danh mục sản phẩm
        /// </summary>
        /// <param name="commmand"></param>
        /// <returns></returns>
        public async Task<int> Create(CreateCategoryCommand commmand)
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
        public async Task<List<CategoryGetAllDto>> GetAll(string languageId)
        {
            var query = from c in _eShopDbContext.Categories
                        join ct in _eShopDbContext.ProductTranslations on c.Id equals ct.ProductId
                        join pic in _eShopDbContext.ProductInCategories on c.Id equals pic.ProductId
                        where ct.LanguageId == languageId
                        select new { c, ct, pic };

            var data = await query.Select(x => new CategoryGetAllDto()
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
        /// <param name="categoryId"></param>
        /// <param name="languageId"></param>
        /// <returns></returns>
        public async Task<CategoryGetAllDto> GetById(int categoryId, string languageId)
        {
            var category = await _eShopDbContext.Categories.FindAsync(categoryId);
            var categoryTranslation = await _eShopDbContext.CategoryTranslations.FirstOrDefaultAsync(x => x.CategoryId == categoryId && x.LanguageId == languageId);

            var categoryViewModel = new CategoryGetAllDto()
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