using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface ILikeService
    {
        Task<IEnumerable<Like>> ListAsync(string userId);
        Task<IEnumerable<Like>> ListAsync(int commentId);
        Task<Like> FindAsync(int commentId, string userId);
        Task<bool> IsUserLiked(int commentId, string userId);
        Task AddAsync(Like like);
        Task DeleteAsync(int commentId, string userId);
    }
}