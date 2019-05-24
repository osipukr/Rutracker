using System.Collections.Generic;

namespace Rutracker.Shared.ViewModels
{
    public class FiltrationViewModel
    {
        public IDictionary<string, bool> ForumTitles { get; set; }
        public long? SizeFrom { get; set; }
        public long? SizeTo { get; set; }
    }
}