using System.Threading.Tasks;
using Rutracker.Client.BusinessLayer.Interfaces;
using Rutracker.Client.BusinessLayer.Settings;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Comment;

namespace Rutracker.Client.BusinessLayer.Services
{
    public class CommentService : ICommentService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ApiUrlOptions _apiUrlOptions;

        public CommentService(HttpClientService httpClientService, ApiUrlOptions apiUrlOptions)
        {
            _httpClientService = httpClientService;
            _apiUrlOptions = apiUrlOptions;
        }

        public async Task<PaginationResult<CommentViewModel>> ListAsync(int page, int pageSize, int torrentId)
        {
            var url = string.Format(_apiUrlOptions.CommentsSearch, torrentId.ToString(), page.ToString(), pageSize.ToString());

            return await _httpClientService.GetJsonAsync<PaginationResult<CommentViewModel>>(url);
        }

        public async Task<CommentViewModel> FindAsync(int id)
        {
            var url = string.Format(_apiUrlOptions.Comment, id.ToString());

            return await _httpClientService.GetJsonAsync<CommentViewModel>(url);
        }

        public async Task<CommentViewModel> AddAsync(CommentCreateViewModel model)
        {
            return await _httpClientService.PostJsonAsync<CommentViewModel>(_apiUrlOptions.Comments, model);
        }

        public async Task<CommentViewModel> UpdateAsync(int id, CommentUpdateViewModel model)
        {
            var url = string.Format(_apiUrlOptions.Comment, id.ToString());

            return await _httpClientService.PutJsonAsync<CommentViewModel>(url, model);
        }

        public async Task DeleteAsync(int id)
        {
            var url = string.Format(_apiUrlOptions.Comment, id.ToString());

            await _httpClientService.DeleteJsonAsync(url);
        }

        public async Task<CommentViewModel> LikeCommentAsync(int id)
        {
            var url = string.Format(_apiUrlOptions.LikeComment, id.ToString());

            return await _httpClientService.GetJsonAsync<CommentViewModel>(url);
        }
    }
}