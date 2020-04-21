using eShopSolution.Api.Application;
using eShopSolution.Api.Application.Commands.Login.Create;
using eShopSolution.Api.Application.Commands.Register.Create;
using eShopSolution.Api.Application.Queries.Users;
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
        Task<string> Auth(CreateLoginCommand command);

        /// <summary>
        /// Đăng kí người dùng
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<bool> Register(CreateRegisterCommand command);

        /// <summary>
        /// Lấy thông tin chi tiết của người dùng
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<GetDetailUserDto> GetProfileUser(GetDetailUserDto query);

        /// <summary>
        /// Lấy ra tất cả người dùng theo param truyền vào
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedResult<UserPagingDto>> GetAllPaging(GetUserPagingRequest request);
    }
}