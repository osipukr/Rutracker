using System.Threading.Tasks;
using Rutracker.Client.Services.Interfaces;
using Rutracker.Client.Settings;
using Rutracker.Shared.ViewModels.Users;

namespace Rutracker.Client.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ApiUriSettings _apiUriSettings;

        public UserService(HttpClientService clientService, ApiUriSettings uriSettings)
        {
            _httpClientService = clientService;
            _apiUriSettings = uriSettings;
        }

        public async Task<UserViewModel[]> Users() => await _httpClientService.GetJsonAsync<UserViewModel[]>(_apiUriSettings.Users);

        public async Task<UserViewModel> UserDetails() => await _httpClientService.GetJsonAsync<UserViewModel>(_apiUriSettings.UserDetails);

        public async Task UpdateUser(EditUserViewModel model) => await _httpClientService.PutJsonAsync(_apiUriSettings.UpdateUser, model);
    }
}