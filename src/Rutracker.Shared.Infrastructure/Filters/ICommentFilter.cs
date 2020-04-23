namespace Rutracker.Shared.Infrastructure.Filters
{
    public interface ICommentFilter : IPagedFilter
    {
        /// <summary>
        /// Torrent id
        /// </summary>
        public int TorrentId { get; }
    }
}