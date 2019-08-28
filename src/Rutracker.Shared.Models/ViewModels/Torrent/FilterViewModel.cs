using System.ComponentModel.DataAnnotations;
using Rutracker.Shared.Models.Resources;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class FilterViewModel
    {
        [Display(Name = nameof(FilterViewModelResource.SearchDisplayName), ResourceType = typeof(FilterViewModelResource))]
        [MaxLength(length: 100,
            ErrorMessageResourceName = nameof(FilterViewModelResource.SearchErrorMessage),
            ErrorMessageResourceType = typeof(FilterViewModelResource))]
        public string Search { get; set; }

        [Display(Name = nameof(FilterViewModelResource.SizeFromDisplayName), ResourceType = typeof(FilterViewModelResource))]
        [Range(minimum: 0, maximum: long.MaxValue,
            ErrorMessageResourceName = nameof(FilterViewModelResource.SizeFromErrorMessage),
            ErrorMessageResourceType = typeof(FilterViewModelResource))]
        public long? SizeFrom { get; set; }

        [Display(Name = nameof(FilterViewModelResource.SizeToDisplayName), ResourceType = typeof(FilterViewModelResource))]
        [Range(minimum: 0, maximum: long.MaxValue,
            ErrorMessageResourceName = nameof(FilterViewModelResource.SizeToErrorMessage),
            ErrorMessageResourceType = typeof(FilterViewModelResource))]
        public long? SizeTo { get; set; }
    }
}