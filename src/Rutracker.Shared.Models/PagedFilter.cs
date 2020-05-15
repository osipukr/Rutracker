using Rutracker.Shared.Infrastructure.Filters;
using Rutracker.Shared.Models.ViewModels;

namespace Rutracker.Shared.Models
{
    public class PagedFilter : View, IPagedFilter
    {
        public int PageIndexFrom { get; set; } = 1;

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}