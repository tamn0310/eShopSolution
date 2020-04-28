using eShopSolution.Api.Application;
using eShopSolution.Api.Application.Commands.Login.Create;
using eShopSolution.Api.Application.Commands.Register.Create;
using eShopSolution.Api.Application.Commands.User;
using eShopSolution.Api.Application.Queries.Users;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserClientApi(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gọi api để thực hiện đăng nhập
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> Auth(CreateLoginCommand command)
        {
            var json = JsonConvert.SerializeObject(command);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.PostAsync("/api/v1/users/auth", httpContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResultSuccess<string>>(await response.Content.ReadAsStringAsync());
            }
            //var new_token = token.Replace('\"', ' ').Trim();
            return JsonConvert.DeserializeObject<ApiResultError<string>>(await response.Content.ReadAsStringAsync()); ;
        }

        /// <summary>
        /// Tạo mới người dùng
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> CreateUser(CreateRegisterCommand command)
        {
            var json = JsonConvert.SerializeObject(command);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.PostAsync("/api/v1/users/register", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiResultSuccess<bool>>(result);
            return JsonConvert.DeserializeObject<ApiResultError<bool>>(result);
        }

        /// <summary>
        /// Call api lấy chi tiết thông tin người dùng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetDetailUserDto>> GetProfile(Guid id)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/v1/users/profile/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiResultSuccess<GetDetailUserDto>>(body);

            return JsonConvert.DeserializeObject<ApiResultError<GetDetailUserDto>>(body);
        }

        /// <summary>
        /// Call api get all user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<PagedResult<UserPagingDto>>> GetUserPaging(GetUserPagingRequest request)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/v1/users/get-all?pageIndex={request.PageIndex}&pageSize={request.PageSize}&search={request.Search}");
            var body = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<ApiResultSuccess<PagedResult<UserPagingDto>>>(body);
            return user;
        }

        /// <summary>
        /// Call api cập nhật người dùng
        /// </summary>
        /// <param name="command"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> UpdateUser(Guid id, UpdateUserCommand command)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(command);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("$/api/v1/users/update/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiResultSuccess<bool>>(result);

            return JsonConvert.DeserializeObject<ApiResultError<bool>>(result);
        }
    }
}