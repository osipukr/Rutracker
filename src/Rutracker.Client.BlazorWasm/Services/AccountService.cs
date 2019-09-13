using System.Threading.Tasks;
using Rutracker.Client.BlazorWasm.Interfaces;
using Rutracker.Client.BlazorWasm.Settings;
using Rutracker.Shared.Models.ViewModels.Account;

namespace Rutracker.Client.BlazorWasm.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ApiUrlSettings _apiUriSettings;
        private readonly ApiAuthenticationStateProvider _apiAuthenticationState;

        public AccountService(
            HttpClientService httpClientService,
            ApiUrlSettings apiUri,
            ApiAuthenticationStateProvider apiAuthenticationState)
        {
            _httpClientService = httpClientService;
            _apiUriSettings = apiUri;
            _apiAuthenticationState = apiAuthenticationState;
        }

        public async Task Login(LoginViewModel model)
        {
            var token = await _httpClientService.PostJsonAsync<string>(_apiUriSettings.Login, model);

            await _apiAuthenticationState.MarkUserAsAuthenticated(token);
        }

        public async Task Register(RegisterViewModel model)
        {
            await _httpClientService.PostJsonAsync(_apiUriSettings.Register, model);
        }

        public async Task CompleteRegistration(CompleteRegistrationViewModel model)
        {
            var token = await _httpClientService.PostJsonAsync<string>(_apiUriSettings.CompleteRegistration, model);

            await _apiAuthenticationState.MarkUserAsAuthenticated(token);
        }

        public async Task Logout()
        {
            await _httpClientService.PostJsonAsync(_apiUriSettings.Logout, null);
            await _apiAuthenticationState.MarkUserAsLoggedOut();
        }
    }
}