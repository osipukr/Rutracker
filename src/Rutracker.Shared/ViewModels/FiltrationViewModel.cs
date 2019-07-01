using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.ViewModels
{
    public class FiltrationViewModel
    {
        [Display(Name = "Search")]
        [StringLength(50, ErrorMessage = "The string must be less than 50 characters.")]
        public string Search { get; set; }

        [Display(Name = "Size From")]
        [Range(0, long.MaxValue, ErrorMessage = "Size From must be greater than 0.")]
        public long? SizeFrom { get; set; }

        [Display(Name = "Size To")]
        [Range(0, long.MaxValue, ErrorMessage = "Size To must be greater than 0.")]
        public long? SizeTo { get; set; }

        [Display(Name = "Forum titles")]
        public IEnumerable<string> SelectedTitleIds { get; set; }

        public FiltrationViewModel()
        {
        }

        public FiltrationViewModel(string search, IEnumerable<string> selectedTitleIds, long? sizeFrom, long? sizeTo)
        {
            Search = search;
            SelectedTitleIds = selectedTitleIds;
            SizeFrom = sizeFrom;
            SizeTo = sizeTo;
        }
    }
}