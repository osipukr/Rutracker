using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Rutracker.Client.Host.Helpers;
using Rutracker.Client.Host.Options;
using Rutracker.Client.Host.Services.Base;
using Rutracker.Client.Infrastructure.Interfaces;
using Rutracker.Shared.Infrastructure.Collections;
using Rutracker.Shared.Infrastructure.Filters;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Client.Host.Services
{
    public class UserService : Service, IUserService
    {
        public UserService(HttpClientService httpClientService, IOptions<ApiOptions> apiOptions) : base(httpClientService, apiOptions)
        {
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

        public async Task<UserDetailView> ChangePasswordAsync(PasswordUpdateView model)
        {
            return await _httpClientService.PutJsonAsync<UserDetailView>(_apiOptions.ChangePassword, model);
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