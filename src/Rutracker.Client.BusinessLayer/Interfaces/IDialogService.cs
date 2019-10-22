using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Dialog;

namespace Rutracker.Client.BusinessLayer.Interfaces
{
    public interface IDialogService
    {
        Task<IEnumerable<DialogViewModel>> ListAsync();
        Task<DialogViewModel> FindAsync(int id);
    }
}