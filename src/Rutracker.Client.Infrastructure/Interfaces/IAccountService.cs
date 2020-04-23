using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Account;

namespace Rutracker.Client.Infrastructure.Interfaces
{
    public interface IAccountService
    {
        Task Login(LoginView model);
        Task Register(RegisterView model);
        Task Logout();
    }
}