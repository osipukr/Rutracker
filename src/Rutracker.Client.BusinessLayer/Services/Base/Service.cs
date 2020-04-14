using Rutracker.Client.BusinessLayer.HttpClient;
using Rutracker.Client.BusinessLayer.Options;

namespace Rutracker.Client.BusinessLayer.Services.Base
{
    public abstract class Service
    {
        protected readonly HttpClientService _httpClientService;
        protected readonly ApiOptions _apiOptions;

        protected Service(HttpClientService httpClientService, ApiOptions apiOptions)
        {
            _httpClientService = httpClientService;
            _apiOptions = apiOptions;
        }
    }
}