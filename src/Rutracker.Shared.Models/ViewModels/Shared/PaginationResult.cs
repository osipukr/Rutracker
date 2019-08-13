namespace Rutracker.Shared.Models.ViewModels.Shared
{
    public class PaginationResult<TResult>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public TResult[] Items { get; set; }
    }
}