using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;

namespace Rutracker.Server.DataAccessLayer.Repositories
{
    public class LikeRepository : Repository<Like, int>, ILikeRepository
    {
        public LikeRepository(RutrackerContext context) : base(context)
        {
        }
    }
}