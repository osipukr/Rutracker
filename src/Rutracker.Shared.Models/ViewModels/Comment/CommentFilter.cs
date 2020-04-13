using Rutracker.Shared.Infrastructure.Interfaces;

namespace Rutracker.Shared.Models.ViewModels.Comment
{
    public class CommentFilter : PagedFilter, ICommentFilter
    {
        public int TorrentId { get; set; }
    }
}