using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Client.Blazor.Interfaces;
using Rutracker.Client.Blazor.Settings;
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

        public async Task<IEnumerable<UserViewModel>> ListAsync()
        {
            return await _httpClientService.GetJsonAsync<IEnumerable<UserViewModel>>(_apiUrls.Users);
        }

        public async Task<UserProfileViewModel> ProfileAsync(string id)
        {
            var url = string.Format(_apiUrls.UserProfile, id);

            return await _httpClientService.GetJsonAsync<UserProfileViewModel>(url);
        }

        public async Task<UserDetailsViewModel> UserDetails()
        {
            return await _httpClientService.GetJsonAsync<UserDetailsViewModel>(_apiUrls.UserDetails);
        }

        public async Task ChangeUser(ChangeUserViewModel model)
        {
            await _httpClientService.PutJsonAsync(_apiUrls.ChangeUser, model);
        }

        public async Task ChangeImage(ChangeImageViewModel model)
        {
            await _httpClientService.PutJsonAsync(_apiUrls.ChangeImage, model);
        }

        public async Task ChangePassword(ChangePasswordViewModel model)
        {
            await _httpClientService.PutJsonAsync(_apiUrls.ChangePassword, model);
        }

        public async Task ChangeEmail(ChangeEmailViewModel model)
        {
            await _httpClientService.PutJsonAsync(_apiUrls.ChangeEmail, model);
        }

        public async Task ChangePhoneNumber(ChangePhoneNumberViewModel model)
        {
            await _httpClientService.PutJsonAsync(_apiUrls.ChangePhoneNumber, model);
        }

        public async Task DeleteImage()
        {
            await _httpClientService.DeleteJsonAsync(_apiUrls.DeleteImage);
        }

        public async Task ConfirmChangeEmail(ConfirmChangeEmailViewModel model)
        {
            await _httpClientService.PostJsonAsync(_apiUrls.ConfirmChangeEmail, model);
        }

        public async Task ConfirmChangePhoneNumber(ConfirmChangePhoneNumberViewModel model)
        {
            await _httpClientService.PostJsonAsync(_apiUrls.ConfirmChangePhoneNumber, model);
        }

        public bool IsValidUserImage(string imageUrl)
        {
            return !string.IsNullOrWhiteSpace(imageUrl);
        }
    }
}