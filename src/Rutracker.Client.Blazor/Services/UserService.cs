using System.Threading.Tasks;
using Rutracker.Client.Blazor.Interfaces;
using Rutracker.Client.Blazor.Settings;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Client.Blazor.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ApiUriSettings _apiUriSettings;

        public UserService(HttpClientService httpClientService, ApiUriSettings uriSettings)
        {
            _httpClientService = httpClientService;
            _apiUriSettings = uriSettings;
        }

        public async Task<UserViewModel[]> Users()
        {
            return await _httpClientService.GetJsonAsync<UserViewModel[]>(_apiUriSettings.Users);
        }

        public async Task<UserDetailsViewModel> UserDetails()
        {
            return await _httpClientService.GetJsonAsync<UserDetailsViewModel>(_apiUriSettings.UserDetails);
        }

        public async Task UpdateUser(EditUserViewModel model)
        {
            await _httpClientService.PutJsonAsync(_apiUriSettings.UpdateUser, model);
        }

        public async Task ChangePassword(ChangePasswordViewModel model)
        {
            await _httpClientService.PostJsonAsync(_apiUriSettings.ChangePassword, model);
        }
    }
}