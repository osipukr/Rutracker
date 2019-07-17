using System.Net.Http;
using System.Threading.Tasks;
using Rutracker.Client.Interfaces;
using Rutracker.Client.Settings;
using Rutracker.Shared.ViewModels.Shared;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Client.Services
{
    public class TorrentsClientService : BaseClientService, ITorrentsClientService
    {
        private readonly ApiUriSettings _apiUris;

        public TorrentsClientService(HttpClient httpClient, ApiUriSettings apiUris)
            : base(httpClient)
        {
            _apiUris = apiUris;
        }

        public async Task<TorrentsIndexViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter)
        {
            var requestUri = string.Format(_apiUris.TorrentsIndex, page, pageSize);

            return await PostJsonAsync<TorrentsIndexViewModel>(requestUri, filter);
        }

        public async Task<TorrentIndexViewModel> GetTorrentIndexAsync(long id)
        {
            var requestUri = string.Format(_apiUris.TorrentIndex, id);

            return await GetJsonAsync<TorrentIndexViewModel>(requestUri);
        }

        public async Task<FacetViewModel<string>> GetTitlesAsync(int count)
        {
            var requestUri = string.Format(_apiUris.Titles, count);

            return await GetJsonAsync<FacetViewModel<string>>(requestUri);
        }
    }
}