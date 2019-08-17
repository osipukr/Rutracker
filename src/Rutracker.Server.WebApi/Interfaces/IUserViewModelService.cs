using System.Security.Claims;
using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Server.WebApi.Interfaces
{
    public interface IUserViewModelService
    {
        Task<UserViewModel[]> UsersAsync();
        Task<UserDetailsViewModel> UserAsync(ClaimsPrincipal principal);
        Task UpdateAsync(ClaimsPrincipal principal, EditUserViewModel model);
        Task ChangePasswordAsync(ClaimsPrincipal principal, ChangePasswordViewModel model);
    }
}