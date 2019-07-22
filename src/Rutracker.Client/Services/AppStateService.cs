using System;
using System.Net.Http;
using System.Threading.Tasks;
using Rutracker.Client.Interfaces;
using Rutracker.Client.Settings;
using Rutracker.Shared.ViewModels.Shared;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Client.Services
{
    public class AppStateService : BaseClientService, ITorrentsClientService
    {
        private readonly ApiUriSettings _apiUris;

        public AppStateService(HttpClient httpClient, ApiUriSettings apiUris)
            : base(httpClient)
        {
            _apiUris = apiUris;
        }

        public bool SearchInProgress { get; private set; }
        public event Action OnChange;

        public async Task<TorrentsIndexViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter)
        {
            SearchInProgress = true;
            NotifyStateChanged();

            try
            {
                var requestUri = string.Format(_apiUris.TorrentsIndex, page, pageSize);

                return await PostJsonAsync<TorrentsIndexViewModel>(requestUri, filter);
            }
            finally
            {
                SearchInProgress = false;
                NotifyStateChanged();
            }
        }

        public async Task<TorrentIndexViewModel> GetTorrentIndexAsync(long id)
        {
            SearchInProgress = true;
            NotifyStateChanged();

            try
            {
                var requestUri = string.Format(_apiUris.TorrentIndex, id);

                return await GetJsonAsync<TorrentIndexViewModel>(requestUri);
            }
            finally
            {
                SearchInProgress = false;
                NotifyStateChanged();
            }
        }

        public async Task<FacetViewModel<string>> GetTitlesAsync(int count)
        {
            var requestUri = string.Format(_apiUris.Titles, count);

            return await GetJsonAsync<FacetViewModel<string>>(requestUri);
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}