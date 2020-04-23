using Microsoft.Extensions.Options;
using Rutracker.Client.Host.Options;

namespace Rutracker.Client.Host.Services.Base
{
    public abstract class Service
    {
        protected readonly HttpClientService _httpClientService;
        protected readonly ApiOptions _apiOptions;

        protected Service(HttpClientService httpClientService, IOptions<ApiOptions> apiOptions)
        {
            _httpClientService = httpClientService;
            _apiOptions = apiOptions.Value;
        }
    }
}