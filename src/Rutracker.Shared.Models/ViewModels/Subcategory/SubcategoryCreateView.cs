using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Subcategory
{
    public class SubcategoryCreateView : View
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int CategoryId { get; set; }
    }
}