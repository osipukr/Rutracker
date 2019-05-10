using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Client.Services
{
    public class TorrentServiceClient
    {
        private readonly HttpClient _client;

        private const string _baseUrl = "http://localhost:60744/api/torrents";
        private const string _torrentsTemplate = _baseUrl + "?search={0}&sort={1}&order={2}&page={3}";
        private const string _torrentDetailsTemplate = _baseUrl + "/details?torrentid={0}";

        public TorrentServiceClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<TorrentsViewModel> GetTorrentsAsync(string search, string sort, string order, int page)
        {
            var requestUrl = string.Format(_torrentsTemplate, search, sort, order, page);

            return await _client.GetJsonAsync<TorrentsViewModel>(requestUrl);
        }

        public async Task<TorrentDetailsViewModel> GetTorrentDetailsAsync(long torrentid)
        {
            var requestUrl = string.Format(_torrentDetailsTemplate, torrentid);

            return await _client.GetJsonAsync<TorrentDetailsViewModel>(requestUrl);
        }
    }
}