using System.Net.Http;

namespace Rutracker.Client.Services
{
    public abstract class BaseClientService
    {
        protected readonly HttpClient _httpClient;

        protected BaseClientService(HttpClient httpClient) => _httpClient = httpClient;
    }
}