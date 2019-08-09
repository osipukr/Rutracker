using System.Threading.Tasks;
using Rutracker.Client.Services.Interfaces;
using Rutracker.Client.Settings;
using Rutracker.Shared.ViewModels.Shared;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Client.Services
{
    public class TorrentService : ITorrentService
    {
        private readonly HttpClientService _httpClient;
        private readonly ApiUriSettings _uriSettings;

        public TorrentService(HttpClientService httpClient, ApiUriSettings uriSettings)
        {
            _httpClient = httpClient;
            _uriSettings = uriSettings;
        }

        public async Task<TorrentsIndexViewModel> Torrents(int page, int pageSize, FiltrationViewModel filter)
        {
            var url = string.Format(_uriSettings.TorrentsIndex, page.ToString(), pageSize.ToString());

            return await _httpClient.PostJsonAsync<TorrentsIndexViewModel>(url, filter);
        }

        public async Task<TorrentIndexViewModel> Torrent(long id)
        {
            var url = string.Format(_uriSettings.TorrentIndex, id.ToString());

            return await _httpClient.GetJsonAsync<TorrentIndexViewModel>(url);
        }

        public async Task<FacetViewModel<string>> TitleFacet(int count)
        {
            var url = string.Format(_uriSettings.Titles, count.ToString());

            return await _httpClient.GetJsonAsync<FacetViewModel<string>>(url);
        }
    }
}