using Rutracker.Shared.Models.ViewModels.Torrent.Base;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class SubcategoryViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public int TorrentsCount { get; set; }
    }
}