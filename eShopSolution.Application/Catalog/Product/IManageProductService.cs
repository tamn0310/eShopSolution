using eShopSolution.Dtos.Catalog.ProductImage;
using eShopSolution.Dtos.Catalog.Products;
using eShopSolution.Dtos.Common;
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
        /// Lấy ra sản phẩm thông qua id tương ứng
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="languageId"></param>
        /// <returns></returns>
        Task<ProductViewModel> GetById(int productId, string languageId);

        /// <summary>
        /// Cập nhật giá tiền cho sản phẩm
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="newPrice"></param>
        /// <returns>Trả về true false</returns>
        Task<bool> UpdatePrice(UpdateProductPriceCommand command);

        /// <summary>
        /// Cập nhật số lượng sản phẩm
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="addedQuantity"></param>
        /// <returns>Trả về true false</returns>
        Task<bool> UpdateStock(UpdateStockProductCommand command);

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
        Task<int> AddImage(int productId, CreateProductImageCommand command);

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
        Task<int> UpdateImage(int imageId, UpdateProductImageCommand command);

        /// <summary>
        /// Lấy ra tất cả ảnh của sản phẩm thông qua id sản phẩm
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<List<ProductImageViewModel>> GetListImage(int productId);

        /// <summary>
        /// Lấy ra thông tin chi tiết ảnh thông qua id truyền vào
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        Task<ProductImageViewModel> GetImageById(int productId, int imageId);
    }
}