using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blazor.FileReader;
using Rutracker.Client.BlazorWasm.Services;
using Rutracker.Shared.Models.ViewModels.File;
using Rutracker.Shared.Models.ViewModels.Torrent;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Client.BlazorWasm.Helpers
{
    public static class HttpClientServiceExtensions
    {
        public static async Task<TResult> PostUserImageFileAsync<TResult>(this HttpClientService httpClientService, string url, IFileReference file)
        {
            var fileStream = await file.OpenReadAsync();
            var fileInfo = await file.ReadFileInfoAsync();

            using var contents = new MultipartFormDataContent();
            using var fileContent = new StreamContent(fileStream);

            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(fileInfo.Type);

            contents.Add(fileContent, nameof(ChangeImageFileViewModel.File), fileInfo.Name);

            return await httpClientService.PostAsync<TResult>(url, contents);
        }

        public static async Task<TResult> PostTorrentImageFileAsync<TResult>(this HttpClientService httpClientService, string url, IFileReference file)
        {
            var fileStream = await file.OpenReadAsync();
            var fileInfo = await file.ReadFileInfoAsync();

            using var contents = new MultipartFormDataContent();
            using var fileContent = new StreamContent(fileStream);

            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(fileInfo.Type);

            contents.Add(fileContent, nameof(ChangeTorrentImageFileViewModel.File), fileInfo.Name);

            return await httpClientService.PostAsync<TResult>(url, contents);
        }

        public static async Task<TResult> PostTorrentFileAsync<TResult>(
            this HttpClientService httpClientService,
            string url,
            int torrentId,
            IFileReference file)
        {
            var fileStream = await file.OpenReadAsync();
            var fileInfo = await file.ReadFileInfoAsync();

            using var contents = new MultipartFormDataContent();
            using var fileContent = new StreamContent(fileStream);

            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(fileInfo.Type);

            contents.Add(fileContent, nameof(FileCreateViewModel.File), fileInfo.Name);

            contents.Add(new StringContent(torrentId.ToString()), nameof(FileCreateViewModel.TorrentId));

            return await httpClientService.PostAsync<TResult>(url, contents);
        }
    }
}