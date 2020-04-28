using eShopSolution.Api.Application;
using eShopSolution.Api.Application.Commands.Login.Create;
using eShopSolution.Api.Application.Commands.Register.Create;
using eShopSolution.Api.Application.Commands.User;
using eShopSolution.Api.Application.Queries.Users;
using System;
using System.Threading.Tasks;

namespace eShopSolution.Application.User
{
    public interface IUserHandler
    {
        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<ApiResult<string>> Auth(CreateLoginCommand command);

        /// <summary>
        /// Đăng kí người dùng
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> Register(CreateRegisterCommand command);

        /// <summary>
        /// Lấy thông tin chi tiết của người dùng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResult<GetDetailUserDto>> GetProfileUser(Guid id);

        /// <summary>
        /// Lấy ra tất cả người dùng theo param truyền vào
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<PagedResult<UserPagingDto>>> GetAllPaging(GetUserPagingRequest request);

        /// <summary>
        /// cập nhật người dùng
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> Update(Guid id, UpdateUserCommand command);
    }
}