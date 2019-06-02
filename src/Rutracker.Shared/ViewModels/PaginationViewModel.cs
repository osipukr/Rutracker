using System;

namespace Rutracker.Shared.ViewModels
{
    public class PaginationViewModel
    {
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int) Math.Ceiling(TotalItems / (double) PageSize);
        public bool HasPrevious => CurrentPage > 1 && CurrentPage <= TotalPages;
        public bool HasNext => CurrentPage < TotalPages;
    }
}    