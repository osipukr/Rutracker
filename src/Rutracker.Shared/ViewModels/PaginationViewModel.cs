namespace Rutracker.Shared.ViewModels
{
    public class PaginationViewModel
    {
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
    }
}    