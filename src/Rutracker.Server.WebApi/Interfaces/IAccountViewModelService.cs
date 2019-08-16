using System.Threading.Tasks;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Account;

namespace Rutracker.Server.WebApi.Interfaces
{
    public interface IAccountViewModelService
    {
        Task<JwtToken> LoginAsync(LoginViewModel model);
        Task<JwtToken> RegisterAsync(RegisterViewModel model);
        Task LogoutAsync();
    }
}