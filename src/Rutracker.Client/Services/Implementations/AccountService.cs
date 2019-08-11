using System.Threading.Tasks;
using Rutracker.Client.Services.Interfaces;
using Rutracker.Client.Settings;
using Rutracker.Shared.ViewModels.Accounts;

namespace Rutracker.Client.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly HttpClientService _httpClient;
        private readonly ApiUriSettings _apiUri;

        public AccountService(HttpClientService httpClient, ApiUriSettings apiUri)
        {
            _httpClient = httpClient;
            _apiUri = apiUri;
        }

        public async Task<JwtToken> Login(LoginViewModel model) => await _httpClient.PostJsonAsync<JwtToken>(_apiUri.Login, model);

        public async Task<JwtToken> Register(RegisterViewModel model) => await _httpClient.PostJsonAsync<JwtToken>(_apiUri.Register, model);

        public async Task Logout() => await _httpClient.PostJsonAsync<JwtToken>(_apiUri.Logout, null);
    }
}