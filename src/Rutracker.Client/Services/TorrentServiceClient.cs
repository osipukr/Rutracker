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

        private readonly string _torrentsTemplate = "api/torrent/torrents?page={0}&pageSize={1}&search={2}";
        private readonly string _torrentTemplate = "api/torrent/details?id={0}";
        private readonly string _titlesTemplate = "api/torrent/titles?count={0}";

        public TorrentServiceClient(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<TorrentsViewModel> GetTorrentsAsync(int page, int pageSize, string search)
        {
            var requestUri = string.Format(_torrentsTemplate, page, pageSize, search);

            return await _httpClient.GetJsonAsync<TorrentsViewModel>(requestUri);
        }

        public async Task<TorrentViewModel> GetTorrentAsync(long id)
        {
            var requestUri = string.Format(_torrentTemplate, id);

            return await _httpClient.GetJsonAsync<TorrentViewModel>(requestUri);
        }

        public async Task<IEnumerable<FacetItem>> GetTitlesAsync(int count)
        {
            var requestUri = string.Format(_titlesTemplate, count);

            return await _httpClient.GetJsonAsync<IEnumerable<FacetItem>>(requestUri);
        }
    }
}