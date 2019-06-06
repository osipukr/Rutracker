using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.ViewModels
{
    public class FiltrationViewModel
    {
        [StringLength(20)]
        public string Search { get; set; }

        [Range(0, long.MaxValue)]
        public int? SizeFrom { get; set; }

        [Range(0, long.MaxValue)]
        public int? SizeTo { get; set; }

        public string[] SelectedTitles { get; set; }
    }
}