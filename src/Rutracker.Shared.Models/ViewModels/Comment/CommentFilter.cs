using Rutracker.Shared.Infrastructure.Filters;

namespace Rutracker.Shared.Models.ViewModels.Comment
{
    public class CommentFilter : PagedFilter, ICommentFilter
    {
        public int TorrentId { get; set; }
    }
}