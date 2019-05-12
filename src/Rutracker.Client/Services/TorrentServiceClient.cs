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

            var baseUrl = uriHelper.GetBaseUri() + "api/torrents/";
            _torrentsTemplate = baseUrl + "{0}/{1}?search={2}&sort={3}&order={4}";
            _torrentDetailsTemplate = baseUrl + "details?torrentid={0}";
        }

        public async Task<TorrentsViewModel> GetTorrentsAsync(string search, string sort, string order, int pageSize, int page)
        {
            var requestUri = string.Format(_torrentsTemplate, pageSize, page, search, sort, order);

            return await _httpClient.GetJsonAsync<TorrentsViewModel>(requestUri);
        }

        public async Task<DetailsViewModel> GetTorrentDetailsAsync(long torrentid)
        {
            var requestUri = string.Format(_torrentDetailsTemplate, torrentid);

            return await _httpClient.GetJsonAsync<DetailsViewModel>(requestUri);
        }
    }
}