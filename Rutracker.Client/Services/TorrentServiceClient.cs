using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Client.Services
{
    public class TorrentServiceClient
    {
        private readonly HttpClient _httpClient;

        private readonly string _torrentsTemplate;
        private readonly string _torrentDetailsTemplate;

        public TorrentServiceClient(HttpClient client, IUriHelper uriHelper)
        {
            _httpClient = client;

            var baseUrl = uriHelper.GetBaseUri() + "api/torrents";
            _torrentsTemplate = baseUrl + "?search={0}&sort={1}&order={2}&page={3}";
            _torrentDetailsTemplate = baseUrl + "/details?torrentid={0}";
        }

        public async Task<TorrentsViewModel> GetTorrentsAsync(string search, string sort, string order, int page)
        {
            var requestUri = string.Format(_torrentsTemplate, search, sort, order, page);

            return await _httpClient.GetJsonAsync<TorrentsViewModel>(requestUri);
        }

        public async Task<TorrentDetailsViewModel> GetTorrentDetailsAsync(long torrentid)
        {
            var requestUri = string.Format(_torrentDetailsTemplate, torrentid);

            return await _httpClient.GetJsonAsync<TorrentDetailsViewModel>(requestUri);
        }
    }
}