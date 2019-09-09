using System.ComponentModel.DataAnnotations;
using Rutracker.Shared.Models.Resources;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class FilterViewModel
    {
        [MaxLength(length: 100,
            ErrorMessageResourceName = nameof(FilterViewModelResource.SearchErrorMessage),
            ErrorMessageResourceType = typeof(FilterViewModelResource))]
        public string Search { get; set; }

        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
    }
}