using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Rutracker.Client.Constants;
using Rutracker.Client.Interfaces;
using Rutracker.Client.Response;
using Rutracker.Shared.ViewModels.Shared;
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
            var response = await _httpClient.PostJsonAsync<OkResponse<TorrentsIndexViewModel>>(requestUri, filter);

            return response.Value;
        }

        public async Task<TorrentIndexViewModel> GetTorrentIndexAsync(long id)
        {
            var requestUri = string.Format(ApiUris.TorrentIndex, id);
            var response = await _httpClient.GetJsonAsync<OkResponse<TorrentIndexViewModel>>(requestUri);

            return response.Value;
        }

        public async Task<FacetViewModel<string>> GetTitlesAsync(int count)
        {
            var requestUri = string.Format(ApiUris.Titles, count);
            var response = await _httpClient.GetJsonAsync<OkResponse<FacetViewModel<string>>>(requestUri);

            return response.Value;
        }
    }
}