using System.Net.Http;
using System.Threading.Tasks;
using Rutracker.Client.Blazor.Extensions;
using Rutracker.Client.Blazor.Interfaces;
using Rutracker.Client.Blazor.Settings;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Account;

namespace Rutracker.Client.Blazor.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiUriSettings _apiUriSettings;

        public AccountService(HttpClient httpClient, ApiUriSettings apiUri)
        {
            _httpClient = httpClient;
            _apiUriSettings = apiUri;
        }

        public async Task<JwtToken> Login(LoginViewModel model)
        {
            return await _httpClient.ApiPostAsync<JwtToken>(_apiUriSettings.Login, model);
        }

        public async Task<JwtToken> Register(RegisterViewModel model)
        {
            return await _httpClient.ApiPostAsync<JwtToken>(_apiUriSettings.Register, model);
        }

        public async Task Logout()
        {
            await _httpClient.ApiPostAsync(_apiUriSettings.Logout, null);
        }
    }
}