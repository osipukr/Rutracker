namespace Rutracker.Shared.Infrastructure.Filters
{
    public interface ITorrentFilter : IPagedFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public int? CategoryId { get; }

        /// <summary>
        /// 
        /// </summary>
        public int? SubcategoryId { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Search { get; }
    }
}