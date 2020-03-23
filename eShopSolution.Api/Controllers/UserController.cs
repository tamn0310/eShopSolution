using eShopSolution.Application.User;
using eShopSolution.Dtos.User;
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
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        /// <summary>
        /// Đăng nhập - tạo token
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("users/auth")]
        public async Task<IActionResult> Auth([FromForm]LoginCommandDto command)
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

            return Ok(new { access_token = resultToken });
        }

        /// <summary>
        /// Đăng kí người dùng
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("users/register")]
        public async Task<IActionResult> Register([FromForm]RegisterCommandDto command)
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