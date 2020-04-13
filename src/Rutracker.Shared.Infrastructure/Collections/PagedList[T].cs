using System;
using System.Collections.Generic;
using System.Linq;

namespace Rutracker.Shared.Infrastructure.Collections
{
    public class PagedList<T> : IPagedList<T>
    {
        /// <summary>
        /// Gets or sets the index of the page.
        /// </summary>
        /// <value>The index of the page.</value>
        public int Page { get; protected set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; protected set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>The total count.</value>
        public int TotalCount { get; protected set; }

        /// <summary>
        /// Gets or sets the total pages.
        /// </summary>
        /// <value>The total pages.</value>
        public int TotalPages { get; protected set; }

        /// <summary>
        /// Gets or sets the index from.
        /// </summary>
        /// <value>The index from.</value>
        public int PageIndexFrom { get; protected set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public IList<T> Items { get; protected set; }

        /// <summary>
        /// Gets the has previous page.
        /// </summary>
        /// <value>The has previous page.</value>
        public bool HasPreviousPage => Page - PageIndexFrom > 0;

        /// <summary>
        /// Gets the has next page.
        /// </summary>
        /// <value>The has next page.</value>
        public bool HasNextPage => Page - PageIndexFrom + 1 < TotalPages;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}" /> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="pageIndex">The index of the page.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <param name="indexFrom">The index from.</param>
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int indexFrom)
        {
            if (indexFrom > pageIndex)
            {
                throw new ArgumentException($"indexFrom: {indexFrom} > pageIndex: {pageIndex}, must indexFrom <= pageIndex");
            }

            Page = pageIndex;
            PageSize = pageSize;
            PageIndexFrom = indexFrom;

            var skip = (Page - PageIndexFrom) * PageSize;

            if (source is IQueryable<T> queryable)
            {
                TotalCount = queryable.Count();

                Items = queryable.Skip(skip).Take(PageSize).ToList();
            }
            else
            {
                TotalCount = source.Count();

                Items = source.Skip(skip).Take(PageSize).ToList();
            }

            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}" /> class.
        /// </summary>
        public PagedList() => Items = new List<T>();
    }
}