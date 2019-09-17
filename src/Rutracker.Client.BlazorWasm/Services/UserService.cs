using System;
using System.Threading.Tasks;
using Blazor.FileReader;
using Rutracker.Client.BlazorWasm.Helpers;
using Rutracker.Client.BlazorWasm.Interfaces;
using Rutracker.Client.BlazorWasm.Settings;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Client.BlazorWasm.Services
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
            var url = string.Format(_apiUrls.UsersSearch, page.ToString(), pageSize.ToString());

            return await _httpClientService.GetJsonAsync<PaginationResult<UserViewModel>>(url);
        }

        public async Task<UserProfileViewModel> ProfileAsync(string userName)
        {
            var url = string.Format(_apiUrls.UserProfile, userName);

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

        public async Task<string> ChangeImageAsync(ChangeImageViewModel model)
        {
            return await _httpClientService.PutJsonAsync<string>(_apiUrls.ChangeImage, model);
        }

        public async Task<string> ChangeImageAsync(IFileReference file)
        {
            if (file == null)
            {
                throw new Exception("File is not selected.");
            }

            var fileInfo = await file.ReadFileInfoAsync();

            if (fileInfo.Size > Constants.File.MaxImageSize)
            {
                throw new Exception("File is too large");
            }

            var stream = await file.OpenReadAsync();

            return await _httpClientService.PostUserImageFileAsync<string>(_apiUrls.ChangeImage, fileInfo.Type, fileInfo.Name, stream);
        }

        public async Task DeleteImageAsync()
        {
            await _httpClientService.DeleteJsonAsync(_apiUrls.ChangeImage);
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