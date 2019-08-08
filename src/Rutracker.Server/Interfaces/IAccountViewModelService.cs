using System.Threading.Tasks;
using Rutracker.Shared.ViewModels.Accounts;

namespace Rutracker.Server.Interfaces
{
    public interface IAccountViewModelService
    {
        Task<JwtToken> LoginAsync(LoginViewModel model);
        Task<JwtToken> RegisterAsync(RegisterViewModel model);
        Task LogoutAsync();
    }
}