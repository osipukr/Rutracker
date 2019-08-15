using System.Threading.Tasks;
using Rutracker.Client.Blazor.Interfaces;
using Rutracker.Client.Blazor.Settings;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Account;

namespace Rutracker.Client.Blazor.Services
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

        public async Task<JwtToken> Login(LoginViewModel model)
        {
            return await _httpClientService.PostJsonAsync<JwtToken>(_apiUriSettings.Login, model);
        }

        public async Task<JwtToken> Register(RegisterViewModel model)
        {
            return await _httpClientService.PostJsonAsync<JwtToken>(_apiUriSettings.Register, model);
        }

        public async Task Logout()
        {
            await _httpClientService.PostJsonAsync(_apiUriSettings.Logout, null);
        }
    }
}