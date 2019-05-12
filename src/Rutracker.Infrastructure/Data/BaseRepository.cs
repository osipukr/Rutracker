namespace Rutracker.Infrastructure.Data
{
    public abstract class BaseRepository
    {
        protected readonly TorrentContext _context;

        protected BaseRepository(TorrentContext context)
        {
            _context = context;
        }
    }
}