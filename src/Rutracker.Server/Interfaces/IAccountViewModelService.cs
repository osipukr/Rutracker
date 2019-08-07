using System.Threading.Tasks;
using Rutracker.Shared.ViewModels.Accounts;
using Rutracker.Shared.ViewModels.Accounts.Response;

namespace Rutracker.Server.Interfaces
{
    public interface IAccountViewModelService
    {
        Task<LoginResponseViewModel> LoginAsync(LoginViewModel model);
        Task RegisterAsync(RegisterViewModel model);
        Task ConfirmRegistrationAsync(ConfirmEmailViewModel model);
        Task LogoutAsync();
    }
}