using System.Threading.Tasks;
using Rutracker.Client.Services.Interfaces;
using Rutracker.Client.Settings;
using Rutracker.Shared.ViewModels.Accounts;

namespace Rutracker.Client.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ApiUriSettings _apiUriSettings;

        public AccountService(HttpClientService httpClient, ApiUriSettings apiUri)
        {
            _httpClientService = httpClient;
            _apiUriSettings = apiUri;
        }

        public async Task<JwtToken> Login(LoginViewModel model) => await _httpClientService.PostJsonAsync<JwtToken>(_apiUriSettings.Login, model);

        public async Task<JwtToken> Register(RegisterViewModel model) => await _httpClientService.PostJsonAsync<JwtToken>(_apiUriSettings.Register, model);

        public async Task Logout() => await _httpClientService.PostJsonAsync(_apiUriSettings.Logout, null);
    }
}