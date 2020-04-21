using eShopSolution.Api.Application.Queries.Products;
using eShopSplution.AdminWeb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace eShopSplution.AdminWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductClientApi _productClientApi;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserController> _logger;

        public ProductController(IProductClientApi productClientApi, IConfiguration configuration, ILogger<UserController> logger)
        {
            _productClientApi = productClientApi;
            _configuration = configuration;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// show all list product
        /// </summary>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string search, int page = 1, int limit = 10)
        {
            var session = HttpContext.Session.GetString("Token");
            var request = new GetManageProductPagingRequest()
            {
                PageIndex = page,
                PageSize = limit,
                Keyword = search,
                BearerToken = session,
            };
            var data = await _productClientApi.GetAll(request);

            return View(data);
        }
    }
}