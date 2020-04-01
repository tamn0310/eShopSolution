using eShopSolution.Api.Application.Commands.Login.Create;
using eShopSolution.Api.Application.Commands.Register.Create;
using eShopSolution.Application.User;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eShopSolution.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserHandler _userService;

        public UserController(IUserHandler userService)
        {
            this._userService = userService;
        }

        /// <summary>
        /// Đăng nhập - tạo token
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("users/auth")]
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
    }
}