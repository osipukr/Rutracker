using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> ListAsync(int torrentId, int count);
        Task<Comment> FindAsync(int commentId, string userId);
        Task<Comment>  AddAsync(Comment comment);
        Task<Comment> UpdateAsync(int commentId, string userId, Comment comment);
        Task<Comment> DeleteAsync(int commentId, string userId);
    }
}