using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Rutracker.Shared.Resources.ViewModels;

namespace Rutracker.Shared.ViewModels.Shared
{
    public class FiltrationViewModel
    {
        [StringLength(maximumLength: 50,
            ErrorMessageResourceName = nameof(FiltrationViewModelResource.SearchError),
            ErrorMessageResourceType = typeof(FiltrationViewModelResource))]
        public string Search { get; set; }

        [Range(minimum: 0.0,
            maximum: long.MaxValue,
            ErrorMessageResourceName = nameof(FiltrationViewModelResource.SizeFromError),
            ErrorMessageResourceType = typeof(FiltrationViewModelResource))]
        public long? SizeFrom { get; set; }

        [Range(minimum: 0.0,
            maximum: long.MaxValue,
            ErrorMessageResourceName = nameof(FiltrationViewModelResource.SizeToError),
            ErrorMessageResourceType = typeof(FiltrationViewModelResource))]
        public long? SizeTo { get; set; }

        public IEnumerable<string> SelectedTitleIds { get; set; }
    }
}