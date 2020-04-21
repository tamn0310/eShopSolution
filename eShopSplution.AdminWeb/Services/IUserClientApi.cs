using eShopSolution.Api.Application;
using eShopSolution.Api.Application.Commands.Login.Create;
using eShopSolution.Api.Application.Queries.Users;
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
        Task<string> Auth(CreateLoginCommand command);

        /// <summary>
        /// Lấy ra tất cả người dùng thep api
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedResult<UserPagingDto>> GetUserPaging(GetUserPagingRequest request);
    }
}