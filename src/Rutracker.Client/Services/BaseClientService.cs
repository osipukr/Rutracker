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
            var result = await ReadContentAsync(response);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ReadStringError(result));
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
            var result = await ReadContentAsync(response);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ReadStringError(result));
            }

            return DeserializeJson<TResult>(result);
        }

        private static async Task<string> ReadContentAsync(HttpResponseMessage response)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            var content = await response.Content.ReadAsStringAsync();

            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            return content;
        }

        private static string ReadStringError(string value)
        {
            var length = value.Length;

            return length > 1 ? value.Substring(1, length - 2) : value;
        }

        private static TResult DeserializeJson<TResult>(string json) => JsonConvert.DeserializeObject<TResult>(json);
    }
}