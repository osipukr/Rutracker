using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Rutracker.Client.Blazor.Services
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClientService _httpClientService;
        private readonly ILocalStorageService _localStorageService;

        private const string TokenKey = "authToken";
        private const string AuthenticationType = "jwt";

        public ApiAuthenticationStateProvider(HttpClientService httpClientService, ILocalStorageService localStorageService)
        {
            _httpClientService = httpClientService;
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorageService.GetItemAsync<string>(TokenKey);

            if (string.IsNullOrWhiteSpace(token))
            {
                _httpClientService.RemoveAuthorizationToken();

                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            _httpClientService.SetAuthorizationToken(token);

            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), AuthenticationType);

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
            await _localStorageService.SetItemAsync(TokenKey, token);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorageService.RemoveItemAsync(TokenKey);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string token) => new JwtSecurityToken(token).Claims;
    }
}