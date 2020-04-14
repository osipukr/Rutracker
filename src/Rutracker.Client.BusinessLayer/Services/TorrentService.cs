using System.Threading.Tasks;
using Rutracker.Client.BusinessLayer.Extensions;
using Rutracker.Client.BusinessLayer.HttpClient;
using Rutracker.Client.BusinessLayer.Interfaces;
using Rutracker.Client.BusinessLayer.Options;
using Rutracker.Client.BusinessLayer.Services.Base;
using Rutracker.Shared.Infrastructure.Collections;
using Rutracker.Shared.Infrastructure.Filters;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Client.BusinessLayer.Services
{
    public class TorrentService : Service, ITorrentService
    {
        public TorrentService(HttpClientService httpClientService, ApiOptions apiOptions) : base(httpClientService, apiOptions)
        {
        }

        public async Task<IPagedList<TorrentView>> ListAsync(ITorrentFilter filter)
        {
            var url = string.Format(_apiOptions.Torrents, filter?.ToQueryString());

            return await _httpClientService.GetJsonAsync<IPagedList<TorrentView>>(url);
        }

        public async Task<TorrentDetailView> FindAsync(int id)
        {
            var url = string.Format(_apiOptions.Torrent, id.ToString());

            return await _httpClientService.GetJsonAsync<TorrentDetailView>(url);
        }

        public async Task<TorrentDetailView> AddAsync(TorrentCreateView model)
        {
            return await _httpClientService.PostJsonAsync<TorrentDetailView>(_apiOptions.Torrents, model);
        }

        public async Task<TorrentDetailView> UpdateAsync(int id, TorrentUpdateView model)
        {
            var url = string.Format(_apiOptions.Torrent, id.ToString());

            return await _httpClientService.PutJsonAsync<TorrentDetailView>(url, model);
        }

        public async Task DeleteAsync(int id)
        {
            var url = string.Format(_apiOptions.Torrent, id.ToString());

            await _httpClientService.DeleteJsonAsync(url);
        }
    }
}