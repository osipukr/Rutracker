using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Comment;

namespace Rutracker.Client.Blazor.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentViewModel>> List(int torrentId, int count);
        Task Add(CommentCreateViewModel model);
        Task Update(int id, CommentUpdateViewModel model);
        Task Delete(int id);
        Task Like(int id);
    }
}