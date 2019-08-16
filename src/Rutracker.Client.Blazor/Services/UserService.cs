using System.Net.Http;
using System.Threading.Tasks;
using Rutracker.Client.Blazor.Extensions;
using Rutracker.Client.Blazor.Interfaces;
using Rutracker.Client.Blazor.Settings;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Client.Blazor.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiUriSettings _apiUriSettings;

        public UserService(HttpClient httpClient, ApiUriSettings uriSettings)
        {
            _httpClient = httpClient;
            _apiUriSettings = uriSettings;
        }

        public async Task<UserViewModel[]> Users()
        {
            return await _httpClient.ApiGetAsync<UserViewModel[]>(_apiUriSettings.Users);
        }

        public async Task<UserDetailsViewModel> UserDetails()
        {
            return await _httpClient.ApiGetAsync<UserDetailsViewModel>(_apiUriSettings.UserDetails);
        }

        public async Task UpdateUser(EditUserViewModel model)
        {
            await _httpClient.ApiPutAsync(_apiUriSettings.UpdateUser, model);
        }
    }
}