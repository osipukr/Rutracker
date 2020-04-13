using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Repositories.Base;

namespace Rutracker.Server.DataAccessLayer.Repositories
{
    public class FileRepository : Repository<File, int>, IFileRepository
    {
        public FileRepository(RutrackerContext context) : base(context)
        {
        }
    }
}