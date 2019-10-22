using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;

namespace Rutracker.Server.DataAccessLayer.Repositories
{
    public class MessageRepository : Repository<Message, int>, IMessageRepository
    {
        public MessageRepository(RutrackerContext context) : base(context)
        {
        }
    }
}