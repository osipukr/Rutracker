using System.Threading.Tasks;
using Rutracker.Client.Blazor.Interfaces;
using Rutracker.Client.Blazor.Settings;
using Rutracker.Shared.Models.ViewModels.Shared;
using Rutracker.Shared.Models.ViewModels.Torrent;
using Rutracker.Shared.Models.ViewModels.Torrents;

namespace Rutracker.Client.Blazor.Services
{
    public class TorrentService : ITorrentService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ApiUriSettings _apiUriSettings;

        public TorrentService(HttpClientService httpClient, ApiUriSettings uriSettings)
        {
            _httpClientService = httpClient;
            _apiUriSettings = uriSettings;
        }

        public async Task<TorrentsIndexViewModel> Torrents(int page, int pageSize, FiltrationViewModel filter)
        {
            var url = string.Format(_apiUriSettings.TorrentsIndex, page.ToString(), pageSize.ToString());

            return await _httpClientService.PostJsonAsync<TorrentsIndexViewModel>(url, filter);
        }

        public async Task<TorrentIndexViewModel> Torrent(long id)
        {
            var url = string.Format(_apiUriSettings.TorrentIndex, id.ToString());

            return await _httpClientService.GetJsonAsync<TorrentIndexViewModel>(url);
        }

        public async Task<FacetViewModel<string>> TitleFacet(int count)
        {
            var url = string.Format(_apiUriSettings.Titles, count.ToString());

            return await _httpClientService.GetJsonAsync<FacetViewModel<string>>(url);
        }
    }
}