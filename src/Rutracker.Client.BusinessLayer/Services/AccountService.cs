using System.Threading.Tasks;
using Rutracker.Client.BusinessLayer.HttpClient;
using Rutracker.Client.BusinessLayer.Interfaces;
using Rutracker.Client.BusinessLayer.Options;
using Rutracker.Client.BusinessLayer.Providers;
using Rutracker.Client.BusinessLayer.Services.Base;
using Rutracker.Shared.Models.ViewModels.Account;

namespace Rutracker.Client.BusinessLayer.Services
{
    public class AccountService : Service, IAccountService
    {
        private readonly ApiAuthenticationStateProvider _apiAuthenticationState;

        public AccountService(
            HttpClientService httpClientService,
            ApiOptions apiOptions,
            ApiAuthenticationStateProvider apiAuthenticationState) : base(httpClientService, apiOptions)
        {
            _apiAuthenticationState = apiAuthenticationState;
        }

        public async Task Login(LoginView model)
        {
            var token = await _httpClientService.PostJsonAsync<string>(_apiOptions.Login, model);

            await _apiAuthenticationState.MarkUserAsAuthenticated(token);
        }

        public async Task Register(RegisterView model)
        {
            await _httpClientService.PostJsonAsync(_apiOptions.Register, model);
        }

        public async Task Logout()
        {
            await _httpClientService.PostJsonAsync(_apiOptions.Logout, null);
            await _apiAuthenticationState.MarkUserAsLoggedOut();
        }
    }
}