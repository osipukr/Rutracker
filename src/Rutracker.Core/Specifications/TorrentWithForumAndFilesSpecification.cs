using Rutracker.Core.Entities;

namespace Rutracker.Core.Specifications
{
    public class TorrentWithForumAndFilesSpecification : BaseSpecification<Torrent, long>
    {
        public TorrentWithForumAndFilesSpecification(long id) 
            : base(x => x.Id == id)
        {
            AddInclude(x => x.Forum);
            AddInclude(x => x.Files);
        }
    }
}