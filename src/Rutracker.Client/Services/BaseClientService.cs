using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Rutracker.Client.Services
{
    public abstract class BaseClientService
    {
        protected readonly HttpClient _httpClient;

        protected BaseClientService(HttpClient httpClient) => _httpClient = httpClient;

        protected async Task<TResult> GetJsonAsync<TResult>(string url)
        {
            using var response = await _httpClient.GetAsync(url);

            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(result);
            }

            return DeserializeJson<TResult>(result);
        }

        protected async Task<TResult> PostJsonAsync<TResult>(string url, object jsonObject)
        {
            using var content = new StringContent(
                JsonConvert.SerializeObject(jsonObject),
                Encoding.UTF8,
                "application/json");

            using var response = await _httpClient.PostAsync(url, content);

            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(result);
            }

            return DeserializeJson<TResult>(result);
        }

        private static TResult DeserializeJson<TResult>(string json) => JsonConvert.DeserializeObject<TResult>(json);
    }
}