using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Rutracker.Client.BlazorWasm.Services;
using Rutracker.Shared.Models.ViewModels.File;
using Rutracker.Shared.Models.ViewModels.Torrent;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Client.BlazorWasm.Helpers
{
    public static class HttpClientServiceExtensions
    {
        private static MultipartFormDataContent BuildFormDataContent(
            string name,
            string mimeType,
            string fileName,
            Stream fileStream)
        {
            var contents = new MultipartFormDataContent();
            var fileContent = new StreamContent(fileStream);

            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(mimeType);

            contents.Add(fileContent, name, fileName);

            return contents;
        }

        public static async Task<TResult> PostUserImageFileAsync<TResult>(
            this HttpClientService httpClientService,
            string url,
            string mimeType,
            string fileName,
            Stream imageStream)
        {
            var contents = BuildFormDataContent(nameof(ChangeImageFileViewModel.File), mimeType, fileName, imageStream);

            return await httpClientService.PostAsync<TResult>(url, contents);
        }

        public static async Task<TResult> PostTorrentImageFileAsync<TResult>(
            this HttpClientService httpClientService,
            string url,
            string mimeType,
            string fileName,
            Stream imageStream)
        {
            using var contents = BuildFormDataContent(nameof(ChangeTorrentImageFileViewModel.File), mimeType, fileName, imageStream);

            return await httpClientService.PostAsync<TResult>(url, contents);
        }

        public static async Task<TResult> PostTorrentFileAsync<TResult>(
            this HttpClientService httpClientService,
            string url,
            int torrentId,
            string mimeType,
            string fileName,
            Stream imageStream)
        {
            var contents = BuildFormDataContent(nameof(FileCreateViewModel.File), mimeType, fileName, imageStream);

            contents.Add(new StringContent(torrentId.ToString()), nameof(FileCreateViewModel.TorrentId));

            return await httpClientService.PostAsync<TResult>(url, contents);
        }
    }
}