using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> ListAsync(int torrentId);
        Task<IEnumerable<Comment>> ListAsync(string userId);
        Task<Comment> FindAsync(int commentId);
        Task<Comment> FindAsync(int commentId, string userId);
        Task AddAsync(Comment comment);
        Task UpdateAsync(int commentId, Comment comment);
    }
}