using System.Threading.Tasks;
using Rutracker.Client.BlazorWasm.Interfaces;
using Rutracker.Client.BlazorWasm.Settings;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Comment;

namespace Rutracker.Client.BlazorWasm.Services
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

        public async Task<PaginationResult<CommentViewModel>> ListAsync(int page, int pageSize, int torrentId)
        {
            var url = string.Format(_apiUrls.CommentsSearch, torrentId.ToString(), page.ToString(), pageSize.ToString());

            return await _httpClientService.GetJsonAsync<PaginationResult<CommentViewModel>>(url);
        }

        public async Task<CommentViewModel> FindAsync(int id)
        {
            var url = string.Format(_apiUrls.Comment, id.ToString());

            return await _httpClientService.GetJsonAsync<CommentViewModel>(url);
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