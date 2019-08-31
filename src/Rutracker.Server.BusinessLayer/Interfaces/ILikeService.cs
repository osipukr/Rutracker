using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface ILikeService
    {
        Task<Like> LikeCommentAsync(int commentId, string userId);
    }
}