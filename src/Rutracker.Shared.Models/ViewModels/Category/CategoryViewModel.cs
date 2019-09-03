using Rutracker.Shared.Models.ViewModels.Subcategory;

namespace Rutracker.Shared.Models.ViewModels.Category
{
    public class CategoryViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public SubcategoryViewModel[] Subcategories { get; set; }
    }
}