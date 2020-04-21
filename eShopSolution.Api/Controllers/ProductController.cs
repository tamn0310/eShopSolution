using eShopSolution.Api.Application.Commands.ProductImages.Create;
using eShopSolution.Api.Application.Commands.ProductImages.Update;
using eShopSolution.Api.Application.Commands.Products.Create;
using eShopSolution.Api.Application.Commands.Products.Update;
using eShopSolution.Api.Application.Commands.Produts.Update;
using eShopSolution.Api.Application.Handler.Catalog.Product;
using eShopSolution.Api.Application.Queries.Products;
using eShopSolution.Api.AppModels;
using eShopSolution.Application.Queries.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace eShopSolution.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    [Produces("application/json")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IManageProductHandler _manageProductHandler;

        public ProductController(ILogger<ProductController> logger,
            IManageProductHandler manageProductHandler)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._manageProductHandler = manageProductHandler;
        }

        /// <summary>
        /// Lấy ra tất cả sản phẩm theo danh mục truyền vào
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Trả ra tất cả bản ghi</response>
        /// <response code="400"></response>
        /// <response code="500">Lỗi server</response>
        /// <returns></returns>
        [HttpGet("products/get-paging-by-cate")]
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPagingByCate([FromQuery]GetPublicProductPagingRequest request)
        {
            try
            {
                var data = await _manageProductHandler.GetAllByCategoryId(request);

                return Ok(data);
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, e);
                throw e;
            }
        }

        /// <summary>
        /// Lấy ra tất cả sản phẩm phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Trả ra tất cả bản ghi</response>
        /// <response code="400"></response>
        /// <response code="500">Lỗi server</response>
        /// <returns></returns>
        [HttpGet("products/get-paging")]
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPaging([FromQuery]GetManageProductPagingRequest request)
        {
            try
            {
                var data = await _manageProductHandler.GetAllPaging(request);

                return Ok(data);
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, e);
                throw e;
            }
        }

        /// <summary>
        /// Lấy ra chi tiết sản phẩm thông qua id sản phẩm và languageId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="languageId"></param>
        /// <returns></returns>
        [HttpGet("products/{id:int}/{languageId}")]
        public async Task<IActionResult> GetById([FromRoute] int id, string languageId)
        {
            var result = await _manageProductHandler.GetById(id, languageId);
            return this.Ok(result);
        }

        /// <summary>
        /// Tạo mới một sản phẩm
        /// </summary>
        /// <response code = "201" > Trả về data của sản phẩm vừa tạo</response>
        /// <response code="400">Nếu các field nhập không đúng định dạng</response>
        /// <response code="500">Lỗi server</response>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("products")]
        [ProducesResponseType(typeof(CreateProductCommand), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromForm] CreateProductCommand request)
        {
            try
            {
                var data = await this._manageProductHandler.Create(request);
                if (data == 0)
                {
                    return BadRequest();
                }

                var result = new ApiResult<int>
                {
                    Message = ApiMessage.CreateOk,
                    Data = data
                };
                return this.Created("api/v1/products", result);
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Cập nhật sản phẩm
        /// </summary>
        /// <response code = "200" > Trả về true</response>
        /// <response code="400">Nếu các field nhập không đúng định dạng</response>
        /// <response code="500">Lỗi server</response>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("products/{id:int}")]
        [ProducesResponseType(typeof(UpdateProductCommand), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromForm]UpdateProductCommand request)
        {
            try
            {
                request.Id = id;
                var updateProduct = await _manageProductHandler.Update(request);
                return this.Ok(updateProduct);
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Xóa sản phẩm theo id truyền vào
        /// </summary>
        /// <response code = "200" > Trả về true</response>
        /// <response code="400">Nếu id truyền vào không đúng</response>
        /// <response code="500">Lỗi server</response>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("products/{id:int}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            try
            {
                var data = await _manageProductHandler.Delete(id);

                var result = new ApiResult<int>
                {
                    Message = ApiMessage.DeleteOk,
                    Data = data
                };

                return this.Ok(result);
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Cập nhật giá tiền của sản phẩm
        /// </summary>
        /// <response code = "200" > Trả về true</response>
        /// <response code="400">Nếu id truyền vào không đúng</response>
        /// <response code="500">Lỗi server</response>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch("products/update-price/{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutPrice(int id, [FromForm]UpdateProductPriceCommand command)
        {
            try
            {
                command.Id = id;
                var updatePrice = await _manageProductHandler.UpdatePrice(command);
                return this.Ok(updatePrice);
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Cập nhật số lượng sản phẩm
        /// </summary>
        /// <response code = "200" > Trả về true</response>
        /// <response code="400">Nếu id truyền vào không đúng</response>
        /// <response code="500">Lỗi server</response>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch("products/update-stock/{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutStock(int id, [FromForm]UpdateStockProductCommand command)
        {
            try
            {
                command.Id = id;
                var updateStock = await _manageProductHandler.UpdateStock(command);
                return this.Ok(updateStock);
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Tạo mới thông tin ảnh cho sản phẩm
        /// </summary>
        /// <response code = "201" > Trả về data của sản phẩm vừa tạo</response>
        /// <response code="400">Nếu các field nhập không đúng định dạng</response>
        /// <response code="500">Lỗi server</response>
        /// <param name="productId"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("products/{productId}/image")]
        [ProducesResponseType(typeof(CreateProductCommand), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateImage(int productId, [FromForm] CreateProductImageCommand command)
        {
            try
            {
                var imageId = await this._manageProductHandler.AddImage(productId, command);
                if (imageId == 0)
                {
                    return BadRequest();
                }

                var image = await _manageProductHandler.GetImageById(productId, imageId);
                return CreatedAtAction(nameof(GetImageById), new { id = productId }, image);
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Lấy ra chi tiết hình ảnh thông qua id truyền vào
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="imageId"></param>
        /// <returns></returns>
        [HttpGet("products/{productId}/image/{imageId}")]
        public async Task<IActionResult> GetImageById(int productId, int imageId)
        {
            try
            {
                var image = await _manageProductHandler.GetImageById(productId, imageId);
                if (image == null)
                {
                    return BadRequest("Cannot find product");
                }
                return Ok(image);
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Cập nhật thông tin ảnh
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("products/{productId}/image/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm]UpdateProductImageCommand command)
        {
            try
            {
                var data = await _manageProductHandler.UpdateImage(imageId, command);
                if (data == 0)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Xóa ảnh theo id
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        [HttpDelete("products/{productId}/image/{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            try
            {
                var result = await _manageProductHandler.RemoveImage(imageId);
                if (result == 0)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, e);
                throw;
            }
        }
    }
}