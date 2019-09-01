using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Client.Blazor.Interfaces;
using Rutracker.Client.Blazor.Settings;
using Rutracker.Shared.Models.ViewModels.Torrent;
using Rutracker.Shared.Models.ViewModels.Torrent.Create;
using Rutracker.Shared.Models.ViewModels.Torrent.Update;

namespace Rutracker.Client.Blazor.Services
{
    public class CommentService : ICommentService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ApiUrlSettings _apiUrls;

        public CommentService(HttpClientService httpClientService, ApiUrlSettings apiUrls)
        {
            _httpClientService = httpClientService;
            _apiUrls = apiUrls;
        }

        public async Task<IEnumerable<CommentViewModel>> List(int torrentId, int count)
        {
            var url = string.Format(_apiUrls.Comments, torrentId.ToString(), count.ToString());

            return await _httpClientService.GetJsonAsync<IEnumerable<CommentViewModel>>(url);
        }

        public async Task Add(CommentCreateViewModel model)
        {
            await _httpClientService.PostJsonAsync(_apiUrls.AddComment, model);
        }

        public async Task Update(int id, CommentUpdateViewModel model)
        {
            var url = string.Format(_apiUrls.UpdateComment, id.ToString());

            await _httpClientService.PutJsonAsync(url, model);
        }

        public async Task Delete(int id)
        {
            var url = string.Format(_apiUrls.UpdateComment, id.ToString());

            await _httpClientService.DeleteJsonAsync(url);
        }

        public async Task Like(int id)
        {
            var url = string.Format(_apiUrls.UpdateComment, id.ToString());

            await _httpClientService.GetJsonAsync(url);
        }
    }
}