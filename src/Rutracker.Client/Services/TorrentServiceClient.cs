using System.Collections.Generic;
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
        private readonly HttpClient _httpClient;

        private const string TorrentsTemplate = "api/torrent/torrents?page={0}&pageSize={1}";
        private const string TorrentTemplate = "api/torrent/details?id={0}";
        private const string TitlesTemplate = "api/torrent/titles?count={0}";

        public TorrentServiceClient(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<TorrentsIndexViewModel> GetTorrentsAsync(int page, int pageSize, FiltrationViewModel filter)
        {
            var requestUri = string.Format(TorrentsTemplate, page, pageSize);

            return await _httpClient.PostJsonAsync<TorrentsIndexViewModel>(requestUri, filter);
        }

        public async Task<TorrentIndexViewModel> GetTorrentAsync(long id)
        {
            var requestUri = string.Format(TorrentTemplate, id);

            return await _httpClient.GetJsonAsync<TorrentIndexViewModel>(requestUri);
        }

        public async Task<IEnumerable<FacetItem>> GetTitlesAsync(int count)
        {
            var requestUri = string.Format(TitlesTemplate, count);

            return await _httpClient.GetJsonAsync<IEnumerable<FacetItem>>(requestUri);
        }
    }
}