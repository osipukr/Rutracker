using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Client.Blazor.Interfaces;
using Rutracker.Client.Blazor.Settings;
using Rutracker.Shared.Models.ViewModels.Comment;

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

        public async Task<IEnumerable<CommentViewModel>> ListAsync(int torrentId)
        {
            var url = $"{_apiUrls.Comments}?{nameof(torrentId)}={torrentId}";

            return await _httpClientService.GetJsonAsync<IEnumerable<CommentViewModel>>(url);
        }

        public async Task<CommentViewModel> AddAsync(CommentCreateViewModel model)
        {
            return await _httpClientService.PostJsonAsync<CommentViewModel>(_apiUrls.Comments, model);
        }

        public async Task<CommentViewModel> UpdateAsync(int id, CommentUpdateViewModel model)
        {
            var url = string.Format(_apiUrls.Comment, id.ToString());

            return await _httpClientService.PutJsonAsync<CommentViewModel>(url, model);
        }

        public async Task DeleteAsync(int id)
        {
            var url = string.Format(_apiUrls.Comment, id.ToString());

            await _httpClientService.DeleteJsonAsync(url);
        }

        public async Task<CommentViewModel> LikeCommentAsync(int id)
        {
            var url = string.Format(_apiUrls.LikeComment, id.ToString());

            return await _httpClientService.GetJsonAsync<CommentViewModel>(url);
        }
    }
}