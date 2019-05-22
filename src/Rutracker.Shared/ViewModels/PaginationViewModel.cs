using System;

namespace Rutracker.Shared.ViewModels
{
    public class PaginationViewModel
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public bool HasPrevious => PageNumber > 1 && PageNumber <= TotalPages;
        public bool HasNext => PageNumber < TotalPages;

        public PaginationViewModel()
        {
        }

        public PaginationViewModel(int totalItems, int pageNumber, int pageSize)
        {
            TotalItems = totalItems;
            PageNumber = pageNumber;
            TotalPages = (int) Math.Ceiling(totalItems / (double) pageSize);
        }
    }
}    