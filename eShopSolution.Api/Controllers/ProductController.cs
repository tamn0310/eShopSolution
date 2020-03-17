using eShopSolution.Api.AppModels;
using eShopSolution.Application.Catalog.Product;
using eShopSolution.Dtos.Catalog.Products;
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
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly ILogger<ProductController> _logger;
        private readonly IManageProductService _manageProductService;

        public ProductController(IPublicProductService publicProductService, ILogger<ProductController> logger,
            IManageProductService manageProductService)
        {
            this._publicProductService = publicProductService;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._manageProductService = manageProductService;
        }

        /// <summary>
        /// Lấy ra tất cả sản phẩm
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Trả ra tất cả bản ghi</response>
        /// <response code="400"></response>
        /// <response code="500">Lỗi server</response>
        [HttpGet("products/{languageId}")]
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string languageId)
        {
            try
            {
                var data = await _publicProductService.GetAll(languageId);

                return Ok(data);
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, e);
                throw e;
            }
        }

        /// <summary>
        /// Lấy ra tất cả sản phẩm theo danh mục truyền vào
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
        public async Task<IActionResult> GetPaging([FromQuery]GetPublicProductPagingRequest request)
        {
            try
            {
                var data = await _publicProductService.GetAllByCategoryId(request);

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
            var result = await _manageProductService.GetById(id, languageId);
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
        [ProducesResponseType(typeof(ProductCreateRequest), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromForm] ProductCreateRequest request)
        {
            try
            {
                var data = await this._manageProductService.Create(request);
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
        [ProducesResponseType(typeof(ProductUpdateRequest), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromForm]ProductUpdateRequest request)
        {
            try
            {
                request.Id = id;
                var updateProduct = await _manageProductService.Update(request);
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
                var data = await _manageProductService.Delete(id);

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
        [HttpPut("products/update-price/{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutPrice(int id, [FromForm]UpdateProductPriceCommand command)
        {
            try
            {
                command.Id = id;
                var updatePrice = await _manageProductService.UpdatePrice(command);
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
        [HttpPut("products/update-stock/{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutStock(int id, [FromForm]UpdateStockProductCommand command)
        {
            try
            {
                command.Id = id;
                var updateStock = await _manageProductService.UpdateStock(command);
                return this.Ok(updateStock);
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, e);
                throw;
            }
        }
    }
}