using System.Threading.Tasks;
using Rutracker.Client.BusinessLayer.Interfaces;
using Rutracker.Client.BusinessLayer.Providers;
using Rutracker.Client.BusinessLayer.Settings;
using Rutracker.Shared.Models.ViewModels.Account;

namespace Rutracker.Client.BusinessLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ApiUrlOptions _apiUrlOptions;
        private readonly ApiAuthenticationStateProvider _apiAuthenticationState;

        public AccountService(HttpClientService httpClientService, ApiUrlOptions apiUrlOptions, ApiAuthenticationStateProvider apiAuthenticationState)
        {
            _httpClientService = httpClientService;
            _apiUrlOptions = apiUrlOptions;
            _apiAuthenticationState = apiAuthenticationState;
        }

        public async Task Login(LoginViewModel model)
        {
            var token = await _httpClientService.PostJsonAsync<string>(_apiUrlOptions.Login, model);

            await _apiAuthenticationState.MarkUserAsAuthenticated(token);
        }

        public async Task Register(RegisterViewModel model)
        {
            await _httpClientService.PostJsonAsync(_apiUrlOptions.Register, model);
        }

        public async Task CompleteRegistration(CompleteRegistrationViewModel model)
        {
            var token = await _httpClientService.PostJsonAsync<string>(_apiUrlOptions.CompleteRegistration, model);

            await _apiAuthenticationState.MarkUserAsAuthenticated(token);
        }

        public async Task Logout()
        {
            await _httpClientService.PostJsonAsync(_apiUrlOptions.Logout, null);
            await _apiAuthenticationState.MarkUserAsLoggedOut();
        }
    }
}