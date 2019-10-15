using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> ListAsync(int dialogId, string userId);
        Task<Message> FindAsync(int id, string userId);
        Task<Message> AddAsync(Message message);
    }
}