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