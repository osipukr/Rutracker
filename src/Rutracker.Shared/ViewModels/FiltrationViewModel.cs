using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.ViewModels
{
    public class FiltrationViewModel
    {
        [Display(Name = "Search")]
        [StringLength(20, ErrorMessage = "The string must be less than 20 characters.")]
        public string Search { get; set; }

        [Display(Name = "Size From")]
        [Range(0, long.MaxValue, ErrorMessage = "Size From must be greater than 0.")]
        public int? SizeFrom { get; set; }

        [Display(Name = "Size To")]
        [Range(0, long.MaxValue, ErrorMessage = "Size To must be greater than 0.")]
        public int? SizeTo { get; set; }

        [Display(Name = "Forum titles")]
        public string[] SelectedTitles { get; set; }
    }
}