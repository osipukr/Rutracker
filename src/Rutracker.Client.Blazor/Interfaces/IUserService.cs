using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.User;
using Rutracker.Shared.Models.ViewModels.User.Change;
using Rutracker.Shared.Models.ViewModels.User.Confirm;

namespace Rutracker.Client.Blazor.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel[]> Users();
        Task<UserDetailsViewModel> UserDetails();
        Task ChangeUser(ChangeUserViewModel model);
        Task ChangeImage(ChangeImageViewModel model);
        Task ChangePassword(ChangePasswordViewModel model);
        Task ChangeEmail(ChangeEmailViewModel model);
        Task ChangePhoneNumber(ChangePhoneNumberViewModel model);
        Task DeleteImage();
        Task ConfirmChangeEmail(ConfirmChangeEmailViewModel model);
        Task ConfirmChangePhoneNumber(ConfirmChangePhoneNumberViewModel model);
    }
}