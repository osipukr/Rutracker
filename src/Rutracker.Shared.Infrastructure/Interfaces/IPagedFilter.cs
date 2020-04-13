namespace Rutracker.Shared.Infrastructure.Interfaces
{
    public interface IPagedFilter
    {
        /// <summary>
        /// Gets the index start value.
        /// </summary>
        /// <value>The index start value.</value>
        int PageIndexFrom { get; }

        /// <summary>
        /// Gets the page index (current).
        /// </summary>
        int Page { get; }

        /// <summary>
        /// Gets the page size.
        /// </summary>
        int PageSize { get; }
    }
}