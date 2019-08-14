using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Client.Blazor.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel[]> Users();
        Task<UserViewModel> UserDetails();
        Task UpdateUser(EditUserViewModel model);
    }
}