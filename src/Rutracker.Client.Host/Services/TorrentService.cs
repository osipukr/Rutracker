using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Rutracker.Client.Host.Helpers;
using Rutracker.Client.Host.Options;
using Rutracker.Client.Host.Services.Base;
using Rutracker.Client.Infrastructure.Interfaces;
using Rutracker.Shared.Infrastructure.Collections;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Client.Host.Services
{
    public class TorrentService : Service, ITorrentService
    {
        public TorrentService(HttpClientService httpClientService, IOptions<ApiOptions> apiOptions) : base(httpClientService, apiOptions)
        {
        }

        public async Task<PagedList<TorrentView>> ListAsync(TorrentFilter filter)
        {
            var url = $"{_apiOptions.Torrents}?{filter?.ToQueryString()}";

            return await _httpClientService.GetJsonAsync<PagedList<TorrentView>>(url);
        }

        public async Task<TorrentView> FindAsync(int id)
        {
            var url = string.Format(_apiOptions.Torrent, id.ToString());

            return await _httpClientService.GetJsonAsync<TorrentView>(url);
        }

        public async Task<TorrentView> AddAsync(TorrentCreateView model)
        {
            return await _httpClientService.PostJsonAsync<TorrentView>(_apiOptions.Torrents, model);
        }

        public async Task<TorrentView> UpdateAsync(int id, TorrentUpdateView model)
        {
            var url = string.Format(_apiOptions.Torrent, id.ToString());

            return await _httpClientService.PutJsonAsync<TorrentView>(url, model);
        }

        public async Task DeleteAsync(int id)
        {
            var url = string.Format(_apiOptions.Torrent, id.ToString());

            await _httpClientService.DeleteJsonAsync(url);
        }
    }
}