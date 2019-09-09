using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface ICommentService
    {
        Task<Tuple<IEnumerable<Comment>, int>> ListAsync(int page, int pageSize, int torrentId);
        Task<Comment> FindAsync(int id, string userId);
        Task<Comment> AddAsync(Comment comment);
        Task<Comment> UpdateAsync(int id, string userId, Comment comment);
        Task<Comment> DeleteAsync(int id, string userId);
        Task<Comment> LikeCommentAsync(int id, string userId);
    }
}