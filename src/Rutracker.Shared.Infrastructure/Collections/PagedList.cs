using System;
using System.Collections.Generic;

namespace Rutracker.Shared.Infrastructure.Collections
{
    /// <summary>
    /// Provides some help methods for <see cref="IPagedList{T}"/> interface.
    /// </summary>
    public static class PagedList
    {
        /// <summary>
        /// Creates an empty of <see cref="IPagedList{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type for paging </typeparam>
        /// <returns>An empty instance of <see cref="IPagedList{T}"/>.</returns>
        public static IPagedList<T> Empty<T>() => new PagedList<T>();

        /// <summary>
        /// Creates a new instance of <see cref="IPagedList{TResult}"/> from source of <see cref="IPagedList{TSource}"/> instance.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="converter">The converter.</param>
        /// <returns>An instance of <see cref="IPagedList{TResult}"/>.</returns>
        public static IPagedList<TResult> From<TResult, TSource>(IPagedList<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
        {
            return new PagedList<TSource, TResult>(source, converter);
        }
    }
}