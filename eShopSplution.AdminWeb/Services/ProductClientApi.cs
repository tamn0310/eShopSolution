using eShopSolution.Api.Application;
using eShopSolution.Api.Application.Queries.Products;
using eShopSolution.Application.Queries.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eShopSplution.AdminWeb.Services
{
    public class ProductClientApi : IProductClientApi
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductClientApi(IConfiguration configuration, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// call api lấy tất cả sản phẩm, phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedResult<ProductViewModel>> GetAll(GetManageProductPagingRequest request)
        {
            var session = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);
            var response = await client.GetAsync($"/api/v1/products/get-paging?pageIndex={request.PageIndex}&pageSize={request.PageSize}&search={request.Keyword}");
            var body = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<PagedResult<ProductViewModel>>(body);
            return product;
        }
    }
}