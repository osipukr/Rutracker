using System.Net.Http;
using System.Threading.Tasks;
using Rutracker.Client.Blazor.Extensions;
using Rutracker.Client.Blazor.Interfaces;
using Rutracker.Client.Blazor.Settings;
using Rutracker.Shared.Models.ViewModels.Shared;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Client.Blazor.Services
{
    public class TorrentService : ITorrentService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiUriSettings _apiUriSettings;

        public TorrentService(HttpClient httpClient, ApiUriSettings uriSettings)
        {
            _httpClient = httpClient;
            _apiUriSettings = uriSettings;
        }

        public async Task<PaginationResult<TorrentViewModel>> Torrents(int page, int pageSize, FilterViewModel filter)
        {
            var url = string.Format(_apiUriSettings.TorrentsIndex, page.ToString(), pageSize.ToString());

            return await _httpClient.ApiPostAsync<PaginationResult<TorrentViewModel>>(url, filter);
        }

        public async Task<TorrentDetailsViewModel> Torrent(long id)
        {
            var url = string.Format(_apiUriSettings.TorrentIndex, id.ToString());

            return await _httpClient.ApiGetAsync<TorrentDetailsViewModel>(url);
        }

        public async Task<FacetResult<string>> TitleFacet(int count)
        {
            var url = string.Format(_apiUriSettings.Titles, count.ToString());

            return await _httpClient.ApiGetAsync<FacetResult<string>>(url);
        }
    }
}