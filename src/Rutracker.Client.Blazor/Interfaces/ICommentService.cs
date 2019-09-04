using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Comment;

namespace Rutracker.Client.Blazor.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentViewModel>> ListAsync(int torrentId);
        Task<CommentViewModel> AddAsync(CommentCreateViewModel model);
        Task<CommentViewModel> UpdateAsync(int id, CommentUpdateViewModel model);
        Task DeleteAsync(int id);
        Task<CommentViewModel> LikeCommentAsync(int id);
    }
}