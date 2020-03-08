using eShopSolution.Dtos.Catalog.Products;
using eShopSolution.Dtos.Catalog.Products.Manage;
using eShopSolution.Dtos.Common;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Product
{
    /// <summary>
    /// quản lí sản phẩm - admin
    /// </summary>
    public interface IManageProductService
    {
        /// <summary>
        /// Thêm mới sản phẩm
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Trả về id sản phẩm</returns>
        Task<int> Create(ProductCreateRequest request);

        /// <summary>
        /// Cập nhật sản phẩm
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Trả về id sản phẩm</returns>
        Task<int> Update(ProductUpdateRequest request);

        /// <summary>
        /// Xóa sản phẩm
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>Trả về id sản phẩm</returns>
        Task<int> Delete(int productId);

        /// <summary>
        /// Cập nhật giá tiền cho sản phẩm
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="newPrice"></param>
        /// <returns>Trả về true false</returns>
        Task<bool> UpdatePrice(int productId, decimal newPrice);

        /// <summary>
        /// Cập nhật số lượng sản phẩm
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="addedQuantity"></param>
        /// <returns>Trả về true false</returns>
        Task<bool> UpdateStock(int productId, int addedQuantity);

        /// <summary>
        /// Method add lượt xem
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task AddViewcount(int productId);

        /// <summary>
        /// Lấy tất cả sản phẩm, phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);

        /// <summary>
        /// Thêm ảnh cho sản phẩm
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        Task<int> AddImgae(int productId, List<IFormFile> files);

        /// <summary>
        /// Xóa ảnh của sản phẩm
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        Task<int> RemoveImage(int imageId);

        /// <summary>
        /// Cập nhật lại ảnh cho sản phẩm
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="Alt"></param>
        /// <param name="IsDefault"></param>
        /// <returns></returns>
        Task<int> UpdateImage(int imageId, string Alt, bool IsDefault);
    }
}