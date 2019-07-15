using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Rutracker.Client.Interfaces;
using Rutracker.Client.Settings;
using Rutracker.Shared.ViewModels.Shared;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Client.Services
{
    public class TorrentsClientService : ITorrentsClientService
    {
        private readonly ApiUriSettings _apiUris;
        private readonly HttpClient _httpClient;

        public TorrentsClientService(HttpClient httpClient, ApiUriSettings apiUris)
        {
            _httpClient = httpClient;
            _apiUris = apiUris;
        }

        public async Task<TorrentsIndexViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter)
        {
            var requestUri = string.Format(_apiUris.TorrentsIndex, page, pageSize);

            return await _httpClient.PostJsonAsync<TorrentsIndexViewModel>(requestUri, filter);
        }

        public async Task<TorrentIndexViewModel> GetTorrentIndexAsync(long id)
        {
            var requestUri = string.Format(_apiUris.TorrentIndex, id);

            return await _httpClient.GetJsonAsync<TorrentIndexViewModel>(requestUri);
        }

        public async Task<FacetViewModel<string>> GetTitlesAsync(int count)
        {
            var requestUri = string.Format(_apiUris.Titles, count);

            return await _httpClient.GetJsonAsync<FacetViewModel<string>>(requestUri);
        }
    }
}