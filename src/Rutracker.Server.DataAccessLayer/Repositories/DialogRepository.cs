using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;

namespace Rutracker.Server.DataAccessLayer.Repositories
{
    public class DialogRepository : Repository<Dialog, int>, IDialogRepository
    {
        public DialogRepository(RutrackerContext context) : base(context)
        {
        }
    }
}