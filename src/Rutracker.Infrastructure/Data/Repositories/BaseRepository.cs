using Rutracker.Infrastructure.Data.Contexts;

namespace Rutracker.Infrastructure.Data.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly TorrentContext _context;

        protected BaseRepository(TorrentContext context) => _context = context;
    }
}