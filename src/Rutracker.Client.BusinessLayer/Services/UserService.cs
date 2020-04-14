using System;
using System.Threading.Tasks;
using Blazor.FileReader;
using Rutracker.Client.BusinessLayer.Extensions;
using Rutracker.Client.BusinessLayer.HttpClient;
using Rutracker.Client.BusinessLayer.Interfaces;
using Rutracker.Client.BusinessLayer.Options;
using Rutracker.Client.BusinessLayer.Services.Base;
using Rutracker.Shared.Infrastructure.Collections;
using Rutracker.Shared.Infrastructure.Filters;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Client.BusinessLayer.Services
{
    public class UserService : Service, IUserService
    {
        private readonly FileOptions _fileOptions;

        public UserService(HttpClientService httpClientService, ApiOptions apiOptions, FileOptions fileOptions) : base(httpClientService, apiOptions)
        {
            _fileOptions = fileOptions;
        }

        public async Task<IPagedList<UserView>> ListAsync(IUserFilter filter)
        {
            var url = string.Format(_apiOptions.Users, filter?.ToQueryString());

            return await _httpClientService.GetJsonAsync<IPagedList<UserView>>(url);
        }

        public async Task<UserView> FindAsync(string userName)
        {
            var url = string.Format(_apiOptions.User, userName);

            return await _httpClientService.GetJsonAsync<UserView>(url);
        }

        public async Task<UserDetailView> FindAsync()
        {
            return await _httpClientService.GetJsonAsync<UserDetailView>(_apiOptions.UserProfile);
        }

        public async Task<UserDetailView> UpdateAsync(UserUpdateView model)
        {
            return await _httpClientService.PutJsonAsync<UserDetailView>(_apiOptions.ChangeUserInfo, model);

        }

        public async Task<string> ChangeImageAsync(ImageUpdateView model)
        {
            return await _httpClientService.PutJsonAsync<string>(_apiOptions.ChangeImage, model);
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

            return null;

            //return await _httpClientService.PostUserImageFileAsync<string>(_apiOptions.ChangeImage, fileInfo.Type, fileInfo.Name, stream);
        }

        public async Task DeleteImageAsync()
        {
            await _httpClientService.DeleteJsonAsync(_apiOptions.ChangeImage);
        }

        public async Task<UserDetailView> ChangePasswordAsync(PasswordUpdateView model)
        {
            return await _httpClientService.PutJsonAsync<UserDetailView>(_apiOptions.ChangePassword, model);
        }

        public async Task ChangeEmailAsync(EmailUpdateView model)
        {
            await _httpClientService.PutJsonAsync(_apiOptions.ChangeEmail, model);
        }

        public async Task ChangePhoneAsync(PhoneUpdateView model)
        {
            await _httpClientService.PutJsonAsync(_apiOptions.ChangePhone, model);
        }

        public async Task ConfirmChangeEmailAsync(EmailConfirmationView model)
        {
            await _httpClientService.PostJsonAsync(_apiOptions.ConfirmChangeEmail, model);
        }

        public async Task<UserDetailView> ConfirmChangePhoneAsync(PhoneConfirmationView model)
        {
            return await _httpClientService.PostJsonAsync<UserDetailView>(_apiOptions.ConfirmPhone, model);
        }

        public async Task ForgotPassword(ForgotPasswordView model)
        {
            await _httpClientService.PostJsonAsync(_apiOptions.ForgotPassword, model);
        }

        public async Task ResetPassword(ResetPasswordView model)
        {
            await _httpClientService.PostJsonAsync(_apiOptions.ResetPassword, model);
        }
    }
}