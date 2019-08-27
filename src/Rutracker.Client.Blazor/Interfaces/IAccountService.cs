using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Account;

namespace Rutracker.Client.Blazor.Interfaces
{
    public interface IAccountService
    {
        Task Login(LoginViewModel model);
        Task Register(RegisterViewModel model);
        Task Logout();
    }
}