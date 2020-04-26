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
            ApiAuthenticationStateProvider apiAuthenticationState,
            IOptions<ApiOptions> apiOptions) : base(httpClientService, apiOptions)
        {
            _apiAuthenticationState = apiAuthenticationState;
        }

        public async Task Login(LoginView model)
        {
            var token = await _httpClientService.PostJsonAsync<TokenView>(_apiOptions.Login, model);

            await _apiAuthenticationState.MarkUserAsAuthenticated(token.Token);
        }

        public async Task Register(RegisterView model)
        {
            var token = await _httpClientService.PostJsonAsync<TokenView>(_apiOptions.Register, model);

            await _apiAuthenticationState.MarkUserAsAuthenticated(token.Token);
        }

        public async Task Logout()
        {
            await _httpClientService.PostJsonAsync(_apiOptions.Logout, null);
            await _apiAuthenticationState.MarkUserAsLoggedOut();
        }

        public async Task ForgotPassword(ForgotPasswordView model)
        {
            await _httpClientService.PostJsonAsync(_apiOptions.ForgotPassword, model);
        }

        public async Task ResetPassword(ResetPasswordView model)
        {
            await _httpClientService.PostJsonAsync(_apiOptions.ResetPassword, model);
        }
    }
}