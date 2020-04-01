using eShopSolution.Api.Application.Commands.Login.Create;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace eShopSplution.AdminWeb.Services
{
    public class UserClientApi : IUserClientApi
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserClientApi(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> Auth(CreateLoginCommand command)
        {
            var json = JsonConvert.SerializeObject(command);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001");
            var response = await client.PostAsync("/api/v1/users/auth", httpContent);
            var token = await response.Content.ReadAsStringAsync();
            var new_token = token.Replace('\"', ' ').Trim();
            return new_token;
        }
    }
}