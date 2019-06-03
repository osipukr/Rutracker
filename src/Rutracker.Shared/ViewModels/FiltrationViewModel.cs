using System.Collections.Generic;

namespace Rutracker.Shared.ViewModels
{
    public class FiltrationViewModel
    {
        public string Search { get; set; }

        public IList<string> SelectedTitles { get; set; }
    }
}