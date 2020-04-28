using eShopSolution.Api.Application;
using eShopSolution.Api.Application.Commands.Login.Create;
using eShopSolution.Api.Application.Commands.Register.Create;
using eShopSolution.Api.Application.Commands.User;
using eShopSolution.Api.Application.Queries.Users;
using System;
using System.Threading.Tasks;

namespace eShopSplution.AdminWeb.Services
{
    public interface IUserClientApi
    {
        /// <summary>
        /// lấy url login
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<ApiResult<string>> Auth(CreateLoginCommand command);

        /// <summary>
        /// Lấy ra tất cả người dùng thep api
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<PagedResult<UserPagingDto>>> GetUserPaging(GetUserPagingRequest request);

        /// <summary>
        /// Tạo mới người dùng
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> CreateUser(CreateRegisterCommand command);

        /// <summary>
        /// Cập nhật người dùng
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> UpdateUser(Guid id, UpdateUserCommand command);

        /// <summary>
        /// call api xem thông tin người dùng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResult<GetDetailUserDto>> GetProfile(Guid id);
    }
}