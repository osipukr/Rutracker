namespace Rutracker.Shared.ViewModels.Shared
{
    public class PaginationViewModel
    {
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasPrevious => CurrentPage > 1 && TotalPages > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}