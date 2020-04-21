using eShopSolution.Api.Application.Commands.Login.Create;
using eShopSolution.Api.Application.Queries.Users;
using eShopSplution.AdminWeb.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSplution.AdminWeb.Controllers
{
    public class UserController : Controller
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

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        /// <summary>
        /// Đăng nhập - post thông tin từ form
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(CreateLoginCommand command)
        {
            if (!ModelState.IsValid)
                return View(ModelState);

            var token = await _userClientApi.Auth(command);

            var userPrincipal = this.ValidateToken(token);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = false
            };

            HttpContext.Session.SetString("Token", token);
            await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        userPrincipal,
                        authProperties);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Login", "User");
        }

        /// <summary>
        /// validation claims
        /// </summary>
        /// <param name="jwtToken"></param>
        /// <returns></returns>
        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            try
            {
                IdentityModelEventSource.ShowPII = true;

                SecurityToken validatedToken;
                TokenValidationParameters validationParameters = new TokenValidationParameters();

                validationParameters.ValidateLifetime = true;

                validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
                validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
                validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

                ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

                return principal;
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, e);
                throw e;
            }
        }
    }
}