using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Client.Blazor.Interfaces;
using Rutracker.Client.Blazor.Settings;
using Rutracker.Shared.Models.ViewModels;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Client.Blazor.Services
{
    public class TorrentService : ITorrentService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ApiUrlSettings _apiUrls;

        public TorrentService(HttpClientService httpClientService, ApiUrlSettings apiUrls)
        {
            _httpClientService = httpClientService;
            _apiUrls = apiUrls;
        }

        public async Task<PaginationResult<TorrentViewModel>> ListAsync(int page, int pageSize, FilterViewModel filter)
        {
            var url = string.Format(_apiUrls.Torrents, page.ToString(), pageSize.ToString());

            return await _httpClientService.PostJsonAsync<PaginationResult<TorrentViewModel>>(url, filter);
        }

        public async Task<IEnumerable<TorrentViewModel>> PopularTorrentsAsync(int count)
        {
            var url = string.Format(_apiUrls.PopularTorrents, count.ToString());

            return await _httpClientService.GetJsonAsync<IEnumerable<TorrentViewModel>>(url);
        }

        public async Task<TorrentDetailsViewModel> FindAsync(int id)
        {
            var url = string.Format(_apiUrls.Torrent, id.ToString());

            return await _httpClientService.GetJsonAsync<TorrentDetailsViewModel>(url);
        }
    }
}