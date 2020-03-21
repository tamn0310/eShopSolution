using eShopSolution.Dtos.User;
using System.Threading.Tasks;

namespace eShopSolution.Application.User
{
    public interface IUserService
    {
        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<string> Auth(LoginCommandDto command);

        /// <summary>
        /// Đăng kí người dùng
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<bool> Register(RegisterCommandDto command);
    }
}