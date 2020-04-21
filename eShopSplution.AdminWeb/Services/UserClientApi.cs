using eShopSolution.Api.Application;
using eShopSolution.Api.Application.Commands.Login.Create;
using eShopSolution.Api.Application.Queries.Users;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eShopSplution.AdminWeb.Services
{
    public class UserClientApi : IUserClientApi
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public UserClientApi(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        /// <summary>
        /// Gọi api để thực hiện đăng nhập
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<string> Auth(CreateLoginCommand command)
        {
            var json = JsonConvert.SerializeObject(command);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.PostAsync("/api/v1/users/auth", httpContent);
            var token = await response.Content.ReadAsStringAsync();
            var new_token = token.Replace('\"', ' ').Trim();
            return new_token;
        }

        /// <summary>
        /// Call api get all user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedResult<UserPagingDto>> GetUserPaging(GetUserPagingRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.BearerToken);
            var response = await client.GetAsync($"/api/v1/users/get-all?pageIndex={request.PageIndex}&pageSize={request.PageSize}&search={request.Search}");
            var body = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<PagedResult<UserPagingDto>>(body);
            return user;
        }
    }
}