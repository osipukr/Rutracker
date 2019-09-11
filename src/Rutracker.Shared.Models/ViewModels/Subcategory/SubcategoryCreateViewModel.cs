using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Subcategory
{
    public class SubcategoryCreateViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
    }
}