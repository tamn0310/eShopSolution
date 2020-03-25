using eShopSolution.Api.Application.Commands.Categories.Create;
using eShopSolution.Api.Application.Queries.Categories.GetAll;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.Api.Application.Handler.Catalog.Category
{
    public interface ICategoryHandler
    {
        /// <summary>
        /// Thêm mới sản phẩm
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Trả về id sản phẩm</returns>
        Task<int> Create(CreateCategoryCommand command);

        /// <summary>
        /// Lấy tất cả danh mục theo languageId truyền vào
        /// </summary>
        /// <param name="languageId"></param>
        /// <returns></returns>
        Task<List<CategoryGetAllDto>> GetAll(string languageId);

        /// <summary>
        /// Lấy ra chi tiết một danh mục theo id và languageId
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="languageId"></param>
        /// <returns></returns>
        Task<CategoryGetAllDto> GetById(int categoryId, string languageId);
    }
}