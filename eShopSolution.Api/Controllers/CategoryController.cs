using eShopSolution.Api.AppModels;
using eShopSolution.Application.Catalog.Category;
using eShopSolution.Dtos.Catalog.Categories.Commands.Create;
using eShopSolution.Dtos.Catalog.Categories.Queries;
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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            this._categoryService = categoryService;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Tạo mới một danh mục sản phẩm
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("categories")]
        [ProducesResponseType(typeof(CategoryCreateCommand), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromForm] CategoryCreateCommand command)
        {
            try
            {
                var data = await this._categoryService.Create(command);
                var result = new ApiResult<int>
                {
                    Message = ApiMessage.CreateOk,
                    Data = data
                };
                return this.Created("api/v1/categories", result);
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Lấy ra tất cả danh mục theo languageId truyền vào
        /// </summary>
        /// <param name="languageId"></param>
        /// <response code="200">Trả ra tất cả bản ghi</response>
        /// <response code="400"></response>
        /// <response code="500">Lỗi server</response>
        /// <returns></returns>
        [HttpGet("categories/{languageId}")]
        [ProducesResponseType(typeof(CategoryQueryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(string languageId)
        {
            try
            {
                var result = await _categoryService.GetAll(languageId);
                return this.Ok(result);
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Lấy ra chi tiết danh mục theo id và languageId truyền vào
        /// </summary>
        /// <param name="id"></param>
        /// <param name="languageId"></param>
        /// <response code="200">Trả ra tất cả bản ghi</response>
        /// <response code="400"></response>
        /// <response code="500">Lỗi server</response>
        /// <returns></returns>
        [HttpGet("categories/{id:int}/{languageId}")]
        [ProducesResponseType(typeof(CategoryQueryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute]int id, string languageId)
        {
            try
            {
                var result = await _categoryService.GetById(id, languageId);
                return this.Ok(result);
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, e);
                throw;
            }
        }
    }
}