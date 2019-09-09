using System.Threading.Tasks;
using Rutracker.Client.Blazor.Interfaces;
using Rutracker.Client.Blazor.Settings;
using Rutracker.Shared.Models.ViewModels;
using Rutracker.Shared.Models.ViewModels.User;
using Rutracker.Shared.Models.ViewModels.User.Change;
using Rutracker.Shared.Models.ViewModels.User.Confirm;

namespace Rutracker.Client.Blazor.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ApiUrlSettings _apiUrls;

        public UserService(HttpClientService httpClientService, ApiUrlSettings apiUrls)
        {
            _httpClientService = httpClientService;
            _apiUrls = apiUrls;
        }

        public async Task<PaginationResult<UserViewModel>> ListAsync(int page, int pageSize)
        {
            var url = string.Format(_apiUrls.Users, page.ToString(), pageSize.ToString());

            return await _httpClientService.GetJsonAsync<PaginationResult<UserViewModel>>(url);
        }

        public async Task<UserProfileViewModel> ProfileAsync(string id)
        {
            var url = string.Format(_apiUrls.UserProfile, id);

            return await _httpClientService.GetJsonAsync<UserProfileViewModel>(url);
        }

        public async Task<UserDetailsViewModel> FindAsync()
        {
            return await _httpClientService.GetJsonAsync<UserDetailsViewModel>(_apiUrls.User);
        }

        public async Task<UserDetailsViewModel> ChangeInfoAsync(ChangeUserViewModel model)
        {
            return await _httpClientService.PutJsonAsync<UserDetailsViewModel>(_apiUrls.ChangeUserInfo, model);
        }

        public async Task<UserDetailsViewModel> ChangeImageAsync(ChangeImageViewModel model)
        {
            return await _httpClientService.PutJsonAsync<UserDetailsViewModel>(_apiUrls.ChangeImage, model);
        }

        public async Task<UserDetailsViewModel> ChangePasswordAsync(ChangePasswordViewModel model)
        {
            return await _httpClientService.PutJsonAsync<UserDetailsViewModel>(_apiUrls.ChangePassword, model);
        }

        public async Task ChangeEmailAsync(ChangeEmailViewModel model)
        {
            await _httpClientService.PutJsonAsync(_apiUrls.ChangeEmail, model);
        }

        public async Task ChangePhoneAsync(ChangePhoneNumberViewModel model)
        {
            await _httpClientService.PutJsonAsync(_apiUrls.ChangePhone, model);
        }

        public async Task ConfirmEmailAsync(ConfirmEmailViewModel model)
        {
            await _httpClientService.PostJsonAsync(_apiUrls.ConfirmEmail, model);
        }

        public async Task ConfirmChangeEmailAsync(ConfirmChangeEmailViewModel model)
        {
            await _httpClientService.PostJsonAsync(_apiUrls.ConfirmChangeEmail, model);
        }

        public async Task<UserDetailsViewModel> ConfirmChangePhoneAsync(ConfirmChangePhoneNumberViewModel model)
        {
            return await _httpClientService.PostJsonAsync<UserDetailsViewModel>(_apiUrls.ConfirmPhone, model);
        }
    }
}