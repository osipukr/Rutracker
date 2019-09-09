using System;
using System.Collections.Generic;

namespace Rutracker.Shared.Models.ViewModels
{
    public class PaginationResult<TResult>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 0;
        public int TotalItems { get; set; } = 0;
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
        public IEnumerable<TResult> Items { get; set; }
    }
}