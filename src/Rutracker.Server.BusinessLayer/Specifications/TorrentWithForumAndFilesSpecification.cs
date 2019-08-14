using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Specifications
{
    public class TorrentWithForumAndFilesSpecification : BaseSpecification<Torrent, long>
    {
        public TorrentWithForumAndFilesSpecification(long id)
            : base(x => x.Id == id)
        {
            base.AddInclude(x => x.Forum);
            base.AddInclude(x => x.Files);
        }
    }
}