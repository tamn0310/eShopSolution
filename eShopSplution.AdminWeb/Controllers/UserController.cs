using eShopSolution.Api.Application.Commands.Register.Create;
using eShopSolution.Api.Application.Queries.Users;
using eShopSplution.AdminWeb.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace eShopSplution.AdminWeb.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserClientApi _userClientApi;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserClientApi userClientApi, IConfiguration configuration, ILogger<UserController> logger)
        {
            _userClientApi = userClientApi;
            _configuration = configuration;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// show all list user
        /// </summary>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string search, int page = 1, int limit = 10)
        {
            var session = HttpContext.Session.GetString("Token");
            var request = new GetUserPagingRequest()
            {
                PageIndex = page,
                PageSize = limit,
                Search = search,
                BearerToken = session
            };
            var data = await _userClientApi.GetUserPaging(request);

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRegisterCommand command)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userClientApi.CreateUser(command);
            if (result)
                return RedirectToAction("Index");

            return View(command);
        }
    }
}