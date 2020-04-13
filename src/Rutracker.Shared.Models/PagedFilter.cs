using Rutracker.Shared.Infrastructure.Filters;

namespace Rutracker.Shared.Models
{
    public class PagedFilter : IPagedFilter
    {
        public int PageIndexFrom { get; set; } = 1;

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}