using System.Threading.Tasks;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Client.Blazor.Interfaces
{
    public interface IUserService
    {
        Task<PaginationResult<UserViewModel>> ListAsync(int page, int pageSize);
        Task<UserProfileViewModel> ProfileAsync(string userName);
        Task<UserDetailsViewModel> FindAsync();
        Task<UserDetailsViewModel> ChangeInfoAsync(ChangeUserViewModel model);
        Task<UserDetailsViewModel> ChangeImageAsync(ChangeImageViewModel model);
        Task<UserDetailsViewModel> ChangePasswordAsync(ChangePasswordViewModel model);
        Task ChangeEmailAsync(ChangeEmailViewModel model);
        Task ChangePhoneAsync(ChangePhoneNumberViewModel model);
        Task ConfirmChangeEmailAsync(ConfirmChangeEmailViewModel model);
        Task<UserDetailsViewModel> ConfirmChangePhoneAsync(ConfirmChangePhoneNumberViewModel model);
    }
}