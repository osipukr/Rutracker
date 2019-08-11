using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Rutracker.Client.Services
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<TResult> GetJsonAsync<TResult>(string url)
        {
            using var response = await _httpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode
                ? DeserializeJson<TResult>(json)
                : throw new Exception(message: DeserializeJsonError(json));
        }

        public async Task<TResult> PostJsonAsync<TResult>(string url, object jsonObject)
        {
            using var content = new StringContent(
                content: JsonConvert.SerializeObject(jsonObject),
                encoding: Encoding.UTF8,
                mediaType: "application/json");

            using var response = await _httpClient.PostAsync(url, content);
            var json = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode
                ? DeserializeJson<TResult>(json)
                : throw new Exception(message: DeserializeJsonError(json));
        }

        public void SetAuthorizationToken(string token)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
        }

        public void RemoveAuthorizationToken()
        {
            if (_httpClient.DefaultRequestHeaders.Authorization != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        private static TResult DeserializeJson<TResult>(string json) => JsonConvert.DeserializeObject<TResult>(json);
        private static string DeserializeJsonError(string json) => DeserializeJson<string>(json);
    }
}