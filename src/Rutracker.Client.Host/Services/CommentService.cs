using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Rutracker.Client.Host.Helpers;
using Rutracker.Client.Host.Options;
using Rutracker.Client.Host.Services.Base;
using Rutracker.Client.Infrastructure.Interfaces;
using Rutracker.Shared.Infrastructure.Collections;
using Rutracker.Shared.Infrastructure.Filters;
using Rutracker.Shared.Models.ViewModels.Comment;

namespace Rutracker.Client.Host.Services
{
    public class CommentService : Service, ICommentService
    {
        public CommentService(HttpClientService httpClientService, IOptions<ApiOptions> apiOptions) : base(httpClientService, apiOptions)
        {
        }

        public async Task<IPagedList<CommentView>> ListAsync(ICommentFilter filter)
        {
            var url = string.Format(_apiOptions.Comments, filter?.ToQueryString());

            return await _httpClientService.GetJsonAsync<IPagedList<CommentView>>(url);
        }

        public async Task<CommentView> FindAsync(int id)
        {
            var url = string.Format(_apiOptions.Comment, id.ToString());

            return await _httpClientService.GetJsonAsync<CommentView>(url);
        }

        public async Task<CommentView> AddAsync(CommentCreateView model)
        {
            return await _httpClientService.PostJsonAsync<CommentView>(_apiOptions.Comments, model);
        }

        public async Task<CommentView> UpdateAsync(int id, CommentUpdateView model)
        {
            var url = string.Format(_apiOptions.Comment, id.ToString());

            return await _httpClientService.PutJsonAsync<CommentView>(url, model);
        }

        public async Task DeleteAsync(int id)
        {
            var url = string.Format(_apiOptions.Comment, id.ToString());

            await _httpClientService.DeleteJsonAsync(url);
        }

        public async Task<CommentView> LikeCommentAsync(int id)
        {
            var url = string.Format(_apiOptions.CommentLike, id.ToString());

            return await _httpClientService.GetJsonAsync<CommentView>(url);
        }
    }
}