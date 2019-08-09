using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Rutracker.Client.Services.Interfaces;
using Rutracker.Shared.ViewModels.Accounts;

namespace Rutracker.Client.Services
{
    public class AuthenticationService : AuthenticationStateProvider
    {
        private readonly IAccountService _accountService;

        public AuthenticationService(IAccountService accountService) => _accountService = accountService;

        public async Task<JwtToken> Login(LoginViewModel model)
        {
            return await _accountService.Login(model);
        }

        public async Task<JwtToken> Register(RegisterViewModel model)
        {
            return await _accountService.Register(model);
        }

        public async Task Logout()
        {
            await _accountService.Logout();
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "mrfibuli"),
            }, "Fake authentication type");

            var user = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(user));
        }
    }
}