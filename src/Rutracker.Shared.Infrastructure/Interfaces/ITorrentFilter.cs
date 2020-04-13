namespace Rutracker.Shared.Infrastructure.Interfaces
{
    public interface ITorrentFilter : IPagedFilter
    {
        public int? CategoryId { get; }

        public int? SubcategoryId { get; }

        public string Search { get; }
    }
}