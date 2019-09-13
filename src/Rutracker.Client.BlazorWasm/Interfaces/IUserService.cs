using System.Threading.Tasks;
using Blazor.FileReader;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Client.BlazorWasm.Interfaces
{
    public interface IUserService
    {
        Task<PaginationResult<UserViewModel>> ListAsync(int page, int pageSize);
        Task<UserProfileViewModel> ProfileAsync(string userName);
        Task<UserDetailsViewModel> FindAsync();
        Task<UserDetailsViewModel> ChangeInfoAsync(ChangeUserViewModel model);
        Task<string> ChangeImageAsync(ChangeImageViewModel model);
        Task<string> ChangeImageAsync(IFileReference fileReference);
        Task DeleteImageAsync();
        Task<UserDetailsViewModel> ChangePasswordAsync(ChangePasswordViewModel model);
        Task ChangeEmailAsync(ChangeEmailViewModel model);
        Task ChangePhoneAsync(ChangePhoneNumberViewModel model);
        Task ConfirmChangeEmailAsync(ConfirmChangeEmailViewModel model);
        Task<UserDetailsViewModel> ConfirmChangePhoneAsync(ConfirmChangePhoneNumberViewModel model);
    }
}