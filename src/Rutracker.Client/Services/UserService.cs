using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Client.Services.Interfaces;
using Rutracker.Client.Settings;
using Rutracker.Shared.ViewModels.Accounts;
using Rutracker.Shared.ViewModels.Users;

namespace Rutracker.Client.Services
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
        public async Task<IReadOnlyList<UserViewModel>> Users(JwtToken token) =>
            await _clientService.GetJsonAsync<IReadOnlyList<UserViewModel>>(_uriSettings.Users, token.Token);

        public async Task<UserResponseViewModel> UserDetails(JwtToken token) =>
            await _clientService.GetJsonAsync<UserResponseViewModel>(_uriSettings.UserDetails, token.Token);
    }
}