using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Rutracker.Client.Host.Options;
using Rutracker.Client.Host.Providers;
using Rutracker.Client.Host.Services.Base;
using Rutracker.Client.Infrastructure.Interfaces;
using Rutracker.Shared.Models.ViewModels.Account;

namespace Rutracker.Client.Host.Services
{
    public class AccountService : Service, IAccountService
    {
        private readonly ApiAuthenticationStateProvider _apiAuthenticationState;

        public AccountService(
            HttpClientService httpClientService,
            IOptions<ApiOptions> apiOptions,
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