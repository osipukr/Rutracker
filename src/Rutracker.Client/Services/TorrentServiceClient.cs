using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Rutracker.Shared.ViewModels;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Client.Services
{
    public class TorrentServiceClient
    {
        private static class ApiUri
        {
            public const string TorrentsIndex = "api/torrent/torrents?page={0}&pageSize={1}";
            public const string TorrentIndex = "api/torrent/details?id={0}";
            public const string Titles = "api/torrent/titles?count={0}";
        }

        private readonly HttpClient _httpClient;

        public TorrentServiceClient(HttpClient client) => _httpClient = client;

        public async Task<TorrentsIndexViewModel> GetTorrentsAsync(int page, int pageSize, FiltrationViewModel filter)
        {
            var requestUri = string.Format(ApiUri.TorrentsIndex, page, pageSize);

            return await _httpClient.PostJsonAsync<TorrentsIndexViewModel>(requestUri, filter);
        }

        public async Task<TorrentIndexViewModel> GetTorrentAsync(long id)
        {
            var requestUri = string.Format(ApiUri.TorrentIndex, id);

            return await _httpClient.GetJsonAsync<TorrentIndexViewModel>(requestUri);
        }

        public async Task<FacetItemViewModel[]> GetTitlesAsync(int count)
        {
            var requestUri = string.Format(ApiUri.Titles, count);

            return await _httpClient.GetJsonAsync<FacetItemViewModel[]>(requestUri);
        }
    }
}