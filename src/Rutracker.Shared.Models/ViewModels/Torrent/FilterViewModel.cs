using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Rutracker.Shared.Models.Resources.ViewModels;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class FilterViewModel
    {
        [StringLength(maximumLength: 100,
            ErrorMessageResourceName = nameof(FilterViewModelResource.SearchError),
            ErrorMessageResourceType = typeof(FilterViewModelResource))]
        public string Search { get; set; }

        [Range(minimum: 0, maximum: long.MaxValue,
            ErrorMessageResourceName = nameof(FilterViewModelResource.SizeFromError),
            ErrorMessageResourceType = typeof(FilterViewModelResource))]
        public long? SizeFrom { get; set; }

        [Range(minimum: 0, maximum: long.MaxValue,
            ErrorMessageResourceName = nameof(FilterViewModelResource.SizeToError),
            ErrorMessageResourceType = typeof(FilterViewModelResource))]
        public long? SizeTo { get; set; }

        public IEnumerable<string> SelectedForumIds { get; set; }
    }
}