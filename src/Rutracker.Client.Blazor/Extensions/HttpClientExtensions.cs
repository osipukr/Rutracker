using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Rutracker.Client.Blazor.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task ApiGetAsync(this HttpClient httpClient, string requestUri)
        {
            using var response = await httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                var message = DeserializeJsonError(await response.Content.ReadAsStringAsync());

                throw new Exception(message);
            }
        }

        public static async Task<TResult> ApiGetAsync<TResult>(this HttpClient httpClient, string requestUri)
        {
            using var response = await httpClient.GetAsync(requestUri);
            var json = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode
                ? DeserializeJson<TResult>(json)
                : throw new Exception(DeserializeJsonError(json));
        }

        public static async Task ApiPostAsync(this HttpClient httpClient, string requestUri, object jsonObject)
        {
            using var content = new StringContent(
                content: JsonConvert.SerializeObject(jsonObject),
                encoding: Encoding.UTF8,
                mediaType: "application/json");

            using var response = await httpClient.PostAsync(requestUri, content);

            if (!response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                throw new Exception(DeserializeJsonError(json));
            }
        }

        public static async Task<TResult> ApiPostAsync<TResult>(this HttpClient httpClient, string requestUri, object jsonObject)
        {
            using var content = new StringContent(
                content: JsonConvert.SerializeObject(jsonObject),
                encoding: Encoding.UTF8,
                mediaType: "application/json");

            using var response = await httpClient.PostAsync(requestUri, content);
            var json = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode
                ? DeserializeJson<TResult>(json)
                : throw new Exception(DeserializeJsonError(json));
        }

        public static async Task ApiPutAsync(this HttpClient httpClient, string requestUri, object jsonObject)
        {
            using var content = new StringContent(
                content: JsonConvert.SerializeObject(jsonObject),
                encoding: Encoding.UTF8,
                mediaType: "application/json");

            using var response = await httpClient.PutAsync(requestUri, content);

            if (!response.IsSuccessStatusCode)
            {
                var message = DeserializeJsonError(await response.Content.ReadAsStringAsync());

                throw new Exception(message);
            }
        }

        public static async Task ApiDeleteAsync(this HttpClient httpClient, string requestUri)
        {
            using var response = await httpClient.DeleteAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                var message = DeserializeJsonError(await response.Content.ReadAsStringAsync());

                throw new Exception(message);
            }
        }

        public static void SetAuthorizationToken(this HttpClient httpClient, string token)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
        }

        public static void RemoveAuthorizationToken(this HttpClient httpClient)
        {
            if (httpClient.DefaultRequestHeaders.Authorization != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        private static TResult DeserializeJson<TResult>(string json) => JsonConvert.DeserializeObject<TResult>(json);
        private static string DeserializeJsonError(string json) => DeserializeJson<string>(json);
    }
}