using System.Threading.Tasks;
using Blazor.FileReader;
using Rutracker.Shared.Infrastructure.Collections;
using Rutracker.Shared.Infrastructure.Filters;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Client.BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<IPagedList<UserView>> ListAsync(IUserFilter filter);
        Task<UserView> FindAsync(string userName);
        Task<UserDetailView> FindAsync();
        Task<UserDetailView> UpdateAsync(UserUpdateView model);
        Task<string> ChangeImageAsync(ImageUpdateView model);
        Task<string> ChangeImageAsync(IFileReference fileReference);
        Task DeleteImageAsync();
        Task<UserDetailView> ChangePasswordAsync(PasswordUpdateView model);
        Task ChangeEmailAsync(EmailUpdateView model);
        Task ChangePhoneAsync(PhoneUpdateView model);
        Task ConfirmChangeEmailAsync(EmailConfirmationView model);
        Task<UserDetailView> ConfirmChangePhoneAsync(PhoneConfirmationView model);
        Task ForgotPassword(ForgotPasswordView model);
        Task ResetPassword(ResetPasswordView model);
    }
}