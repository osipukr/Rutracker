namespace Rutracker.Shared.Infrastructure.Filters
{
    public interface ITorrentFilter : IPagedFilter
    {
        public int? CategoryId { get; }

        public int? SubcategoryId { get; }

        public string Search { get; }
    }
}