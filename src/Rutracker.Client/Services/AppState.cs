using System;
using System.Threading.Tasks;
using Rutracker.Client.Services.Interfaces;
using Rutracker.Client.Settings;
using Rutracker.Shared.ViewModels.Shared;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Client.Services
{
    public class AppState : ITorrentService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ApiUriSettings _apiUris;

        public AppState(HttpClientService httpClientService, ApiUriSettings apiUris)
        {
            _httpClientService = httpClientService ?? throw new ArgumentNullException(nameof(httpClientService));
            _apiUris = apiUris ?? throw new ArgumentNullException(nameof(apiUris));
        }

        public bool SearchInProgress { get; private set; }
        public event Action OnChange;

        public async Task<TorrentsIndexViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter) =>
            await IndexActionAsync(() =>
            {
                var url = string.Format(_apiUris.TorrentsIndex, page.ToString(), pageSize.ToString());

                return _httpClientService.PostJsonAsync<TorrentsIndexViewModel>(url, filter);
            });

        public async Task<TorrentIndexViewModel> GetTorrentIndexAsync(long id) =>
            await IndexActionAsync(() =>
            {
                var url = string.Format(_apiUris.TorrentIndex, id.ToString());

                return _httpClientService.GetJsonAsync<TorrentIndexViewModel>(url);
            });

        public async Task<FacetViewModel<string>> GetTitleFacetAsync(int count)
        {
            var url = string.Format(_apiUris.Titles, count.ToString());

            return await _httpClientService.GetJsonAsync<FacetViewModel<string>>(url);
        }

        private async Task<TResult> IndexActionAsync<TResult>(Func<Task<TResult>> action)
        {
            SearchInProgress = true;
            NotifyStateChanged();

            try
            {
                return await action();
            }
            finally
            {
                SearchInProgress = false;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}