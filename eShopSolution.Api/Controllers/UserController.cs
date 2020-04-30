using eShopSolution.Api.Application.Commands.Login.Create;
using eShopSolution.Api.Application.Commands.Register.Create;
using eShopSolution.Api.Application.Commands.User;
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
            if (string.IsNullOrEmpty(resultToken.Data))
            {
                return BadRequest(resultToken);
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
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Cập nhật người dùng
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("users/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody]UpdateUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Update(id, command);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Lấy thông tin người dùng qua id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetProfile(Guid id)
        {
            try
            {
                var user = await _userService.GetProfileUser(id);
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

        /// <summary>
        /// Xóa người dùng theo id truyền vào
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var user = await _userService.Delete(id);

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