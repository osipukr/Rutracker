using System.Threading.Tasks;
using Rutracker.Client.Services.Interfaces;
using Rutracker.Client.Settings;
using Rutracker.Shared.ViewModels.Users;

namespace Rutracker.Client.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly HttpClientService _clientService;
        private readonly ApiUriSettings _uriSettings;

        public UserService(HttpClientService clientService, ApiUriSettings uriSettings)
        {
            _clientService = clientService;
            _uriSettings = uriSettings;
        }

        public async Task<UserViewModel[]> Users() => await _clientService.GetJsonAsync<UserViewModel[]>(_uriSettings.Users);

        public async Task<UserViewModel> UserDetails() => await _clientService.GetJsonAsync<UserViewModel>(_uriSettings.UserDetails);
    }
}