using System.Collections.Generic;

namespace Rutracker.Shared.ViewModels
{
    public class FiltrationViewModel
    {
        public List<string> SelectedTitles { get; set; }
        public long? SizeFrom { get; set; }
        public long? SizeTo { get; set; }
    }
}