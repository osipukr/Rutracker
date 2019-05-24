using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Rutracker.Shared.ViewModels;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Client.Services
{
    public class TorrentServiceClient
    {
        private readonly HttpClient _httpClient;

        private readonly string _torrentsTemplate;
        private readonly string _torrentDetailsTemplate;
        private readonly string _forumsTemplate;

        public TorrentServiceClient(HttpClient client, IUriHelper uriHelper)
        {
            _httpClient = client;

            var baseUrl = uriHelper.GetBaseUri() + "api/torrent/";
            _torrentsTemplate = baseUrl + "{0}/{1}?search={2}&titles={3}&sizeFrom={4}&sizeTo={5}";
            _torrentDetailsTemplate = baseUrl + "details?id={0}";
            _forumsTemplate = baseUrl + "forums?count={0}";
        }

        public async Task<TorrentsViewModel> GetTorrentsAsync(int pageSize, int page, string search, string titles, long? sizeFrom, long? sizeTo)
        {
            var requestUri = string.Format(_torrentsTemplate, pageSize, page, search, titles, sizeFrom, sizeTo);

            return await _httpClient.GetJsonAsync<TorrentsViewModel>(requestUri);
        }

        public async Task<DetailsViewModel> GetTorrentDetailsAsync(long id)
        {
            var requestUri = string.Format(_torrentDetailsTemplate, id);

            return await _httpClient.GetJsonAsync<DetailsViewModel>(requestUri);
        }

        public async Task<FiltrationViewModel> GetTorrentFilterAsync(int count)
        {
            var requestUri = string.Format(_forumsTemplate, count);

            return await _httpClient.GetJsonAsync<FiltrationViewModel>(requestUri);
        }
    }
}