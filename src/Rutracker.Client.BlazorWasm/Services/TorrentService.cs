using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Client.BlazorWasm.Interfaces;
using Rutracker.Client.BlazorWasm.Settings;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Client.BlazorWasm.Services
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

        public async Task<PaginationResult<TorrentViewModel>> ListAsync(int page, int pageSize, TorrentFilterViewModel filter)
        {
            var url = string.Format(_apiUrls.TorrentsSearch, page.ToString(), pageSize.ToString());

            return await _httpClientService.PostJsonAsync<PaginationResult<TorrentViewModel>>(url, filter);
        }

        public async Task<IEnumerable<TorrentViewModel>> PopularAsync(int count)
        {
            var url = string.Format(_apiUrls.PopularTorrentsSearch, count.ToString());

            return await _httpClientService.GetJsonAsync<IEnumerable<TorrentViewModel>>(url);
        }

        public async Task<TorrentDetailsViewModel> FindAsync(int id)
        {
            var url = string.Format(_apiUrls.Torrent, id.ToString());

            return await _httpClientService.GetJsonAsync<TorrentDetailsViewModel>(url);
        }

        public async Task<TorrentDetailsViewModel> CreateAsync(TorrentCreateViewModel model)
        {
            return await _httpClientService.PostJsonAsync<TorrentDetailsViewModel>(_apiUrls.Torrents, model);
        }

        public async Task<TorrentDetailsViewModel> Update(int id, TorrentUpdateViewModel model)
        {
            var url = string.Format(_apiUrls.Torrent, id.ToString());

            return await _httpClientService.PutJsonAsync<TorrentDetailsViewModel>(url, model);
        }

        public async Task DeleteAsync(int id)
        {
            var url = string.Format(_apiUrls.Torrent, id.ToString());

            await _httpClientService.DeleteJsonAsync(url);
        }
    }
}