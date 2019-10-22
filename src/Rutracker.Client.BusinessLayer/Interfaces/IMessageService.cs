using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Message;

namespace Rutracker.Client.BusinessLayer.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageViewModel>> ListAsync(int dialogId);
        Task<MessageViewModel> FindAsync(int id);
    }
}