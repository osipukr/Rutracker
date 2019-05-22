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
            _torrentsTemplate = baseUrl + "{0}/{1}?search={2}";
            _torrentDetailsTemplate = baseUrl + "details?torrentid={0}";
            _forumsTemplate = baseUrl + "forums?count={0}";
        }

        public async Task<TorrentsViewModel> GetTorrentsAsync(int pageSize, int page, string search)
        {
            var requestUri = string.Format(_torrentsTemplate, pageSize, page, search);

            return await _httpClient.GetJsonAsync<TorrentsViewModel>(requestUri);
        }

        public async Task<DetailsViewModel> GetTorrentDetailsAsync(long torrentid)
        {
            var requestUri = string.Format(_torrentDetailsTemplate, torrentid);

            return await _httpClient.GetJsonAsync<DetailsViewModel>(requestUri);
        }

        public async Task<FiltrationViewModel> GetForumFilterAsync(int count)
        {
            var requestUri = string.Format(_forumsTemplate, count);

            return await _httpClient.GetJsonAsync<FiltrationViewModel>(requestUri);
        }
    }
}