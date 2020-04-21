using eShopSolution.Api.Application.Commands.Login.Create;
using eShopSolution.Api.Application.Commands.Register.Create;
using eShopSolution.Api.Application.Queries.Users;
using eShopSolution.Application.User;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserHandler _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserHandler userService, ILogger<UserController> logger)
        {
            this._userService = userService;
            this._logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        /// <summary>
        /// Đăng nhập - tạo token
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("users/auth")]
        [AllowAnonymous]
        public async Task<IActionResult> Auth([FromBody]CreateLoginCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultToken = await _userService.Auth(command);
            if (string.IsNullOrEmpty(resultToken))
            {
                return BadRequest("user name or password is incorrect");
            }

            return Ok(resultToken);
        }

        /// <summary>
        /// Đăng kí người dùng
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("users/register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]CreateRegisterCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Register(command);
            if (!result)
            {
                return BadRequest("Register is unsuccessful");
            }

            return Ok();
        }

        /// <summary>
        /// Lấy thông tin người dùng qua id
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("users/profile")]
        public async Task<IActionResult> GetProfile([FromForm]GetDetailUserDto query)
        {
            try
            {
                var user = await _userService.GetProfileUser(query);
                return this.Ok(user);
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, e);
                throw e;
            }
        }

        /// <summary>
        /// Hiển thị tất cả người dùng theo param truyền vào
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("users/get-all")]
        public async Task<IActionResult> GetAll([FromQuery] GetUserPagingRequest request)
        {
            try
            {
                var user = await _userService.GetAllPaging(request);
                return Ok(user);
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message, e);
                throw e;
            }
        }
    }
}