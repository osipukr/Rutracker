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
        private readonly ApiAuthenticationStateProvider _apiAuthenticationState;

        public AccountService(
            HttpClientService httpClientService,
            ApiUriSettings apiUri,
            ApiAuthenticationStateProvider apiAuthenticationState)
        {
            _httpClientService = httpClientService;
            _apiUriSettings = apiUri;
            _apiAuthenticationState = apiAuthenticationState;
        }

        public async Task Login(LoginViewModel model)
        {
            var token = await _httpClientService.PostJsonAsync<JwtToken>(_apiUriSettings.Login, model);

            await _apiAuthenticationState.MarkUserAsAuthenticated(token.Token);
        }

        public async Task Register(RegisterViewModel model)
        {
            await _httpClientService.PostJsonAsync(_apiUriSettings.Register, model);
        }

        public async Task Logout()
        {
            await _httpClientService.PostJsonAsync(_apiUriSettings.Logout, null);
            await _apiAuthenticationState.MarkUserAsLoggedOut();
        }
    }
}