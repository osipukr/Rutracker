using System.Linq;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Interfaces
{
    public interface ICommentRepository : IRepository<Comment, int>
    {
        IQueryable<Comment> GetAll(int torrentId);
        IQueryable<Comment> GetAll(string userId);
        Task<Comment> GetAsync(int commentId, string userId);
    }
}