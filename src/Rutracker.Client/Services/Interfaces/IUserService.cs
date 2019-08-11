using System.Threading.Tasks;
using Rutracker.Shared.ViewModels.Users;

namespace Rutracker.Client.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel[]> Users();
        Task<UserViewModel> UserDetails();
    }
}