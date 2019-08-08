using System.Threading.Tasks;
using Rutracker.Shared.ViewModels.Users;

namespace Rutracker.Server.Interfaces
{
    public interface IUsersViewModelService
    {
        Task<UserViewModel> GetUserAsync();
    }
}