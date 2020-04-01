using eShopSolution.Api.Application.Commands.Login.Create;
using System.Threading.Tasks;

namespace eShopSplution.AdminWeb.Services
{
    public interface IUserClientApi
    {
        Task<string> Auth(CreateLoginCommand command);
    }
}