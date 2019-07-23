using System;
using System.Net.Http;
using System.Threading.Tasks;
using Rutracker.Client.Settings;
using Rutracker.Shared.Interfaces;
using Rutracker.Shared.ViewModels.Shared;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Client.Services
{
    public class AppStateService : BaseClientService, ITorrentViewModelService
    {
        private readonly ApiUriSettings _apiUris;

        public AppStateService(HttpClient httpClient, ApiUriSettings apiUris)
            : base(httpClient)
        {
            _apiUris = apiUris;
        }

        public bool SearchInProgress { get; private set; }
        public event Action OnChange;

        public async Task<TorrentsIndexViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter) =>
            await IndexActionAsync(() =>
            {
                var url = string.Format(_apiUris.TorrentsIndex, page.ToString(), pageSize.ToString());

                return PostJsonAsync<TorrentsIndexViewModel>(url, filter);
            });

        public async Task<TorrentIndexViewModel> GetTorrentIndexAsync(long id) =>
            await IndexActionAsync(() =>
            {
                var url = string.Format(_apiUris.TorrentIndex, id.ToString());

                return GetJsonAsync<TorrentIndexViewModel>(url);
            });

        public async Task<FacetViewModel<string>> GetTitleFacetAsync(int count)
        {
            var url = string.Format(_apiUris.Titles, count.ToString());

            return await GetJsonAsync<FacetViewModel<string>>(url);
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