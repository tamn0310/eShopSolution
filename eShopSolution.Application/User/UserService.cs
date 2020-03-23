using eShopSolution.Data.Entities;
using eShopSolution.Dtos.User;
using eShopSolution.Utilities.ExceptionCommon;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager, IConfiguration config)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._config = config;
        }

        /// <summary>
        /// Đăng nhập, tạo token
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<string> Auth(LoginCommandDto command)
        {
            var user = await _userManager.FindByNameAsync(command.UserName);
            if (user == null)
            {
                throw new EShopException("cannot find user name");
            }

            var result = await _signInManager.PasswordSignInAsync(user, command.PassWord, command.RememberMe, true);

            if (!result.Succeeded)
            {
                return null;
            }
            var role = await _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";", role))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var access_token = new JwtSecurityToken(_config["Token:Issuer"],
                _config["Token:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(access_token);
        }

        /// <summary>
        /// Đăng kí người dùng
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<bool> Register(RegisterCommandDto command)
        {
            var user = new AppUser()
            {
                UserName = command.UserName,
                Email = command.Email,
                Dob = command.Dob,
                FirstName = command.FirstName,
                LastName = command.LastName,
                PhoneNumber = command.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, command.PassWord);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }
    }
}