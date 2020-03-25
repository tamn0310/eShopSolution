using eShopSolution.Api.Application.Commands.Login.Create;
using eShopSolution.Api.Application.Commands.Register.Create;
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
    }
}