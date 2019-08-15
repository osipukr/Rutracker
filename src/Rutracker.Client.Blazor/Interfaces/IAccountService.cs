using System.Threading.Tasks;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Account;

namespace Rutracker.Client.Blazor.Interfaces
{
    public interface IAccountService
    {
        Task<JwtToken> Login(LoginViewModel model);
        Task<JwtToken> Register(RegisterViewModel model);
        Task Logout();
    }
}