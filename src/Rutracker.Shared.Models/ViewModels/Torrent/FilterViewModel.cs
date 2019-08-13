using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Rutracker.Shared.Models.Resources.ViewModels;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class FilterViewModel
    {
        [StringLength(maximumLength: 100,
            ErrorMessageResourceName = nameof(FiltrationViewModelResource.SearchError),
            ErrorMessageResourceType = typeof(FiltrationViewModelResource))]
        public string Search { get; set; }

        [Range(minimum: 0, maximum: long.MaxValue,
            ErrorMessageResourceName = nameof(FiltrationViewModelResource.SizeFromError),
            ErrorMessageResourceType = typeof(FiltrationViewModelResource))]
        public long? SizeFrom { get; set; }

        [Range(minimum: 0, maximum: long.MaxValue,
            ErrorMessageResourceName = nameof(FiltrationViewModelResource.SizeToError),
            ErrorMessageResourceType = typeof(FiltrationViewModelResource))]
        public long? SizeTo { get; set; }

        public IEnumerable<string> SelectedForumIds { get; set; }
    }
}