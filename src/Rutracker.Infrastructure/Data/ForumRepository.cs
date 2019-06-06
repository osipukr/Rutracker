using Rutracker.Core.Entities;
using Rutracker.Core.Interfaces;

namespace Rutracker.Infrastructure.Data
{
    public class ForumRepository : Repository<Forum, long>, IForumRepository
    {
        public ForumRepository(TorrentContext context) 
            : base(context)
        {
        }
    }
}