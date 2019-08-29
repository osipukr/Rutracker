using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Interfaces
{
    public interface ICommentRepository : IRepository<Comment, int>
    {
    }
}