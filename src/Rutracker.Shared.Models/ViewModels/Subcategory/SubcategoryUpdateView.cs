using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Subcategory
{
    public class SubcategoryUpdateView : View
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}