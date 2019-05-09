using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Client.Services
{
    public class TorrentServiceClient
    {
        private const string _baseUrl = "/api/torrents";
        private const string _torrentsTemplate = _baseUrl + "?search={0}&sort={1}&order={2}&pageid={3}";
        private const string _torrentDetailsTemplate = _baseUrl + "?torrentid={0}";
        private readonly HttpClient _client;

        public TorrentServiceClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<TorrentsViewModel> Get(string search = "", string sort = "Id", string order = "Asc", int pageid = 1)
        {
            var requestUrl = string.Format(_torrentsTemplate, search, sort, order, pageid);

            return await _client.GetJsonAsync<TorrentsViewModel>(requestUrl);
        }

        public async Task<TorrentDetailsViewModel> GetDetails(string torrentid)
        {
            var requestUrl = string.Format(_torrentDetailsTemplate, torrentid);

            return await _client.GetJsonAsync<TorrentDetailsViewModel>(requestUrl);
        }
    }
}