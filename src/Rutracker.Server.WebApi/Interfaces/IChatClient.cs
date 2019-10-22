using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Dialog;
using Rutracker.Shared.Models.ViewModels.Message;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Server.WebApi.Interfaces
{
    public interface IChatClient
    {
        Task ReceiveDialogAsync(DialogViewModel dialog);
        Task ReceiveMessageAsync(MessageViewModel message);
        Task JoinDialogAsync(UserViewModel user);
        Task LeaveDialogAsync(UserViewModel user);
    }
}