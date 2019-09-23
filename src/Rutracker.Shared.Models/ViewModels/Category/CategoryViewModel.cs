using System.Collections.Generic;
using Rutracker.Shared.Models.ViewModels.Subcategory;

namespace Rutracker.Shared.Models.ViewModels.Category
{
    public class CategoryViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public int SubcategoriesCount { get; set; }
        public IEnumerable<SubcategoryViewModel> Subcategories { get; set; }
    }
}