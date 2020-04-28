using eShopSolution.Api.Application;
using eShopSolution.Api.Application.Commands.Login.Create;
using eShopSolution.Api.Application.Commands.Register.Create;
using eShopSolution.Api.Application.Commands.User;
using eShopSolution.Api.Application.Queries.Users;
using eShopSolution.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.User
{
    public class UserHandler : IUserHandler
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly ILogger<UserHandler> _logger;

        public UserHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager, IConfiguration config,
            ILogger<UserHandler> logger)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._config = config;
            this._logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        /// <summary>
        /// Đăng nhập, tạo token
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> Auth(CreateLoginCommand command)
        {
            var user = await _userManager.FindByNameAsync(command.UserName);
            if (user == null) return null;

            var result = await _signInManager.PasswordSignInAsync(user, command.PassWord, command.RememberMe, true);
            if (!result.Succeeded)
            {
                return null;
            }
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, command.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new ApiResultSuccess<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        /// <summary>
        /// Lấy ra tất cả người dùng phân trang theo param truyền vào
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<PagedResult<UserPagingDto>>> GetAllPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(x => x.UserName.Contains(request.Search) || x.PhoneNumber.Contains(request.Search));
            }
            /*Paging*/
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserPagingDto()
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Address = x.Address,
                    PhoneNumber = x.PhoneNumber,
                    Dob = x.Dob,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                }).ToListAsync();

            /*Select and projection*/
            var pageResult = new PagedResult<UserPagingDto>()
            {
                Total = totalRow,
                Page = request.PageIndex,
                Limit = request.PageSize,
                Items = data
            };

            return new ApiResultSuccess<PagedResult<UserPagingDto>>(pageResult);
        }

        /// <summary>
        /// Lấy thông tin người dùng qua id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetDetailUserDto>> GetProfileUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiResultError<GetDetailUserDto>("Người dùng không tồn tại");
            }
            var userVm = new GetDetailUserDto()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Dob = user.Dob,
                Address = user.Address,
                CreatedAt = user.CreatedAt,
                CreatedBy = user.CreatedBy,
                UpdatedAt = user.UpdatedAt,
                UpdatedBy = user.UpdatedBy
            };

            return new ApiResultSuccess<GetDetailUserDto>(userVm);
        }

        /// <summary>
        /// Đăng kí người dùng
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> Register(CreateRegisterCommand command)
        {
            var user = await _userManager.FindByNameAsync(command.UserName);
            if (user != null)
            {
                return new ApiResultError<bool>("Tài khoản đã tồn tại");
            }

            if (await _userManager.FindByEmailAsync(command.Email) != null)
            {
                return new ApiResultError<bool>("Email đã tồn tại");
            }

            user = new AppUser()
            {
                UserName = command.UserName,
                Email = command.Email,
                Dob = command.Dob,
                FirstName = command.FirstName,
                LastName = command.LastName,
                PhoneNumber = command.PhoneNumber,
                CreatedAt = DateTime.Now,
                CreatedBy = "system"
            };

            var result = await _userManager.CreateAsync(user, command.PassWord);
            if (result.Succeeded)
            {
                return new ApiResultSuccess<bool>();
            }
            return new ApiResultError<bool>("Đăng ký không thành công");
        }

        /// <summary>
        /// Cập nhật người dùng
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> Update(Guid id, UpdateUserCommand command)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == command.Email && x.Id != id))
            {
                return new ApiResultError<bool>("Email đã tồn tại");
            }

            var user = await _userManager.FindByIdAsync(id.ToString());
            user.Email = command.Email;
            user.Dob = command.Dob;
            user.FirstName = command.FirstName;
            user.LastName = command.LastName;
            user.PhoneNumber = command.PhoneNumber;
            user.Address = command.Address;
            user.UpdatedAt = DateTime.Now;
            user.UpdatedBy = "system";

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApiResultSuccess<bool>();
            }
            return new ApiResultError<bool>("Cập nhật không thành công");
        }
    }
}