using System.Security.Claims;
using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Server.WebApi.Interfaces
{
    public interface IUserViewModelService
    {
        Task<UserViewModel[]> GetUsersAsync();
        Task<UserViewModel> GetUserAsync(ClaimsPrincipal principal);
        Task UpdateUserAsync(ClaimsPrincipal principal, EditUserViewModel model);
    }
}