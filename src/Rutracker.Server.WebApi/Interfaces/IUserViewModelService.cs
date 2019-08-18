using System.Security.Claims;
using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Server.WebApi.Interfaces
{
    public interface IUserViewModelService
    {
        Task<UserViewModel[]> UsersAsync();
        Task<UserDetailsViewModel> UserAsync(ClaimsPrincipal principal);
        Task ChangeUserAsync(ClaimsPrincipal principal, ChangeUserViewModel model);
        Task ChangeImageAsync(ClaimsPrincipal principal, ChangeImageViewModel model);
        Task ChangePasswordAsync(ClaimsPrincipal principal, ChangePasswordViewModel model);
        Task ChangeEmailAsync(ClaimsPrincipal principal, ChangeEmailViewModel model);
        Task ChangePhoneNumberAsync(ClaimsPrincipal principal, ChangePhoneNumberViewModel model);
        Task DeleteImageAsync(ClaimsPrincipal principal);
        Task DeletePhoneNumber(ClaimsPrincipal principal);
        Task SendConfirmationEmailAsync(ClaimsPrincipal principal);
        Task ConfirmEmailAsync(ConfirmEmailViewModel model);
    }
}