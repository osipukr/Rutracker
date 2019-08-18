using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.User;

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
        Task SendConfirmationEmail();
    }
}