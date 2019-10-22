using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.FileReader;
using Rutracker.Client.BusinessLayer.Extensions;
using Rutracker.Client.BusinessLayer.Interfaces;
using Rutracker.Client.BusinessLayer.Settings;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Client.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ApiUrlOptions _apiUrlOptions;
        private readonly FileOptions _fileOptions;

        public UserService(HttpClientService httpClientService, ApiUrlOptions apiUrlOptions, FileOptions fileOptions)
        {
            _httpClientService = httpClientService;
            _apiUrlOptions = apiUrlOptions;
            _fileOptions = fileOptions;
        }

        public async Task<PaginationResult<UserViewModel>> ListAsync(int page, int pageSize)
        {
            var url = string.Format(_apiUrlOptions.UsersSearch, page.ToString(), pageSize.ToString());

            return await _httpClientService.GetJsonAsync<PaginationResult<UserViewModel>>(url);
        }

        public async Task<IEnumerable<UserViewModel>> ListAsync(string search)
        {
            var url = string.Format(_apiUrlOptions.UsersFind, search);

            return await _httpClientService.GetJsonAsync<IEnumerable<UserViewModel>>(url);
        }

        public async Task<UserProfileViewModel> ProfileAsync(string userName)
        {
            var url = string.Format(_apiUrlOptions.UserProfile, userName);

            return await _httpClientService.GetJsonAsync<UserProfileViewModel>(url);
        }

        public async Task<UserDetailsViewModel> FindAsync()
        {
            return await _httpClientService.GetJsonAsync<UserDetailsViewModel>(_apiUrlOptions.User);
        }

        public async Task<UserDetailsViewModel> ChangeInfoAsync(ChangeUserViewModel model)
        {
            return await _httpClientService.PutJsonAsync<UserDetailsViewModel>(_apiUrlOptions.ChangeUserInfo, model);
        }

        public async Task<string> ChangeImageAsync(ChangeImageViewModel model)
        {
            return await _httpClientService.PutJsonAsync<string>(_apiUrlOptions.ChangeImage, model);
        }

        public async Task<string> ChangeImageAsync(IFileReference file)
        {
            if (file == null)
            {
                throw new Exception("File is not selected.");
            }

            var fileInfo = await file.ReadFileInfoAsync();

            if (fileInfo.Size > _fileOptions.MaxImageSize)
            {
                throw new Exception("File is too large");
            }

            var stream = await file.OpenReadAsync();

            return await _httpClientService.PostUserImageFileAsync<string>(_apiUrlOptions.ChangeImage, fileInfo.Type, fileInfo.Name, stream);
        }

        public async Task DeleteImageAsync()
        {
            await _httpClientService.DeleteJsonAsync(_apiUrlOptions.ChangeImage);
        }

        public async Task<UserDetailsViewModel> ChangePasswordAsync(ChangePasswordViewModel model)
        {
            return await _httpClientService.PutJsonAsync<UserDetailsViewModel>(_apiUrlOptions.ChangePassword, model);
        }

        public async Task ChangeEmailAsync(ChangeEmailViewModel model)
        {
            await _httpClientService.PutJsonAsync(_apiUrlOptions.ChangeEmail, model);
        }

        public async Task ChangePhoneAsync(ChangePhoneNumberViewModel model)
        {
            await _httpClientService.PutJsonAsync(_apiUrlOptions.ChangePhone, model);
        }

        public async Task ConfirmChangeEmailAsync(ConfirmChangeEmailViewModel model)
        {
            await _httpClientService.PostJsonAsync(_apiUrlOptions.ConfirmChangeEmail, model);
        }

        public async Task<UserDetailsViewModel> ConfirmChangePhoneAsync(ConfirmChangePhoneNumberViewModel model)
        {
            return await _httpClientService.PostJsonAsync<UserDetailsViewModel>(_apiUrlOptions.ConfirmPhone, model);
        }
    }
}