using System.Threading.Tasks;
using Rutracker.Shared.Infrastructure.Collections;
using Rutracker.Shared.Models.ViewModels.Comment;

namespace Rutracker.Client.Infrastructure.Interfaces
{
    public interface ICommentService
    {
        Task<PagedList<CommentView>> ListAsync(CommentFilter filter);
        Task<CommentView> FindAsync(int id);
        Task<CommentView> AddAsync(CommentCreateView model);
        Task<CommentView> UpdateAsync(int id, CommentUpdateView model);
        Task DeleteAsync(int id);
        Task<CommentView> LikeCommentAsync(int id);
    }
}