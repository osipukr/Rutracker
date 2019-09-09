using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.User;
using Rutracker.Shared.Models.ViewModels.User.Change;
using Rutracker.Shared.Models.ViewModels.User.Confirm;

namespace Rutracker.Client.Blazor.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> ListAsync();
        Task<UserProfileViewModel> ProfileAsync(string id);
        Task<UserDetailsViewModel> FindAsync();
        Task<UserDetailsViewModel> ChangeInfoAsync(ChangeUserViewModel model);
        Task<UserDetailsViewModel> ChangeImageAsync(ChangeImageViewModel model);
        Task<UserDetailsViewModel> ChangePasswordAsync(ChangePasswordViewModel model);
        Task ChangeEmailAsync(ChangeEmailViewModel model);
        Task ChangePhoneAsync(ChangePhoneNumberViewModel model);
        Task ConfirmEmailAsync(ConfirmEmailViewModel model);
        Task ConfirmChangeEmailAsync(ConfirmChangeEmailViewModel model);
        Task<UserDetailsViewModel> ConfirmChangePhoneAsync(ConfirmChangePhoneNumberViewModel model);
    }
}