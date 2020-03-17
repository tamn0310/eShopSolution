using eShopSolution.Dtos.Catalog.Categories.Commands.Create;
using eShopSolution.Dtos.Catalog.Categories.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Category
{
   public interface ICategoryService
    {
        /// <summary>
        /// Thêm mới sản phẩm
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Trả về id sản phẩm</returns>
        Task<int> Create(CategoryCreateCommand commnad);

        /// <summary>
        /// Lấy tất cả danh mục theo languageId truyền vào
        /// </summary>
        /// <param name="languageId"></param>
        /// <returns></returns>
        Task<List<CategoryQueryDto>> GetAll(string languageId);

        /// <summary>
        /// Lấy ra chi tiết một danh mục theo id và languageId
        /// </summary>
        /// <param name="languageId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CategoryQueryDto> GetById(int categoryId, string languageId);
    }
}
