using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Rutracker.Client.Constants;
using Rutracker.Client.Interfaces;
using Rutracker.Shared.ViewModels;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Client.Services
{
    public class TorrentsClientService : BaseClientService, ITorrentsClientService
    {
        public TorrentsClientService(HttpClient client)
            : base(client)
        {
        }

        public async Task<TorrentsIndexViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter)
        {
            var requestUri = string.Format(ApiUris.TorrentsIndex, page, pageSize);
            var result = await _httpClient.PostJsonAsync<TorrentsIndexViewModel>(requestUri, filter);

            return result;
        }

        public async Task<TorrentIndexViewModel> GetTorrentIndexAsync(long id)
        {
            var requestUri = string.Format(ApiUris.TorrentIndex, id);
            var result = await _httpClient.GetJsonAsync<TorrentIndexViewModel>(requestUri);

            return result;
        }

        public async Task<FacetItemViewModel[]> GetTitlesAsync(int count)
        {
            var requestUri = string.Format(ApiUris.Titles, count);
            var result = await _httpClient.GetJsonAsync<FacetItemViewModel[]>(requestUri);

            return result;
        }
    }
}