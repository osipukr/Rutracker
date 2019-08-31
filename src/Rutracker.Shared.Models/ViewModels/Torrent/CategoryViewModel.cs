using Rutracker.Shared.Models.ViewModels.Torrent.Base;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class CategoryViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public SubcategoryViewModel[] Subcategories { get; set; }
    }
}