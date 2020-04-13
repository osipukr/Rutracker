namespace Rutracker.Shared.Infrastructure.Interfaces
{
    public interface ICommentFilter : IPagedFilter
    {
        public int TorrentId { get; }
    }
}