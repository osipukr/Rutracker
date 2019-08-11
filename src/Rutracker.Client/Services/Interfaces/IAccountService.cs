using System.Threading.Tasks;
using Rutracker.Shared.ViewModels.Accounts;

namespace Rutracker.Client.Services.Interfaces
{
    public interface IAccountService
    {
        Task<JwtToken> Login(LoginViewModel model);
        Task<JwtToken> Register(RegisterViewModel model);
        Task Logout();
    }
}