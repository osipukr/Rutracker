using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Infrastructure.Collections;
using Rutracker.Shared.Infrastructure.Filters;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface ICommentService
    {
        Task<IPagedList<Comment>> ListAsync(ICommentFilter filter);
        Task<Comment> FindAsync(int id);
        Task<Comment> FindAsync(int id, string userId);
        Task<Comment> AddAsync(Comment comment);
        Task<Comment> UpdateAsync(int id, string userId, Comment comment);
        Task<Comment> DeleteAsync(int id, string userId);
        Task<Comment> LikeCommentAsync(int id, string userId);
    }
}