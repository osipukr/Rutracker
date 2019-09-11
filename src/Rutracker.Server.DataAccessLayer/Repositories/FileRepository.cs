using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;

namespace Rutracker.Server.DataAccessLayer.Repositories
{
    public class FileRepository : Repository<File, int>, IFileRepository
    {
        public FileRepository(RutrackerContext context) : base(context)
        {
        }
    }
}