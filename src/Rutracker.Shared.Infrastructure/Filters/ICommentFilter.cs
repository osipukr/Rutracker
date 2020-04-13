namespace Rutracker.Shared.Infrastructure.Filters
{
    public interface ICommentFilter : IPagedFilter
    {
        public int TorrentId { get; }
    }
}