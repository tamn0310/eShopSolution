using eShopSolution.Application.Catalog.Product;
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

        public ProductController(IPublicProductService publicProductService, ILogger<ProductController> logger)
        {
            this._publicProductService = publicProductService;
            this._logger = logger;
        }

        /// <summary>
        /// Lấy ra tất cả sản phẩm
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Trả ra tất cả bản ghi</response>
        /// <response code="400"></response>
        /// <response code="500">Lỗi server</response>
        [HttpGet("products")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = await _publicProductService.GetAll();
                return Ok(products);
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, e);
                throw e;
            }
        }
    }
}