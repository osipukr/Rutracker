using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ProtectedBrowserStorage;
using Rutracker.Client.Host.Helpers;
using Rutracker.Client.Host.Services;

namespace Rutracker.Client.Host.Providers
{
    public class ApiAuthenticationStateProvider : ServerAuthenticationStateProvider
    {
        private readonly HttpClientService _httpClientService;
        private readonly ProtectedLocalStorage _storage;
        private readonly IHttpContextAccessor _accessor;

        private const string TokenKey = "authToken";
        private const string AuthenticationType = "jwt";

        public ApiAuthenticationStateProvider(HttpClientService httpClientService, ProtectedLocalStorage localStorage, IHttpContextAccessor accessor)
        {
            _httpClientService = httpClientService;
            _storage = localStorage;
            _accessor = accessor;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = string.Empty;

            if (_accessor.IsServerStarted())
            {
                token = await _storage.GetAsync<string>(TokenKey);
            }

            Console.WriteLine($"Token is {token}");

            if (!string.IsNullOrWhiteSpace(token))
            {
                var jwtToken = new JwtSecurityToken(token);

                if (jwtToken.ValidTo > DateTime.UtcNow)
                {
                    _httpClientService.SetAuthorization(token);

                    var identity = new ClaimsIdentity(jwtToken.Claims, AuthenticationType);

                    return new AuthenticationState(new ClaimsPrincipal(identity));
                }
            }

            _httpClientService.RemoveAuthorization();

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
            if (_accessor.IsServerStarted())
            {
                await _storage.SetAsync(TokenKey, token);
            }

            SetAuthenticationState(GetAuthenticationStateAsync());
        }

        public async Task MarkUserAsLoggedOut()
        {
            if (_accessor.IsServerStarted())
            {
                await _storage.DeleteAsync(TokenKey);
            }

            SetAuthenticationState(GetAuthenticationStateAsync());
        }
    }
}