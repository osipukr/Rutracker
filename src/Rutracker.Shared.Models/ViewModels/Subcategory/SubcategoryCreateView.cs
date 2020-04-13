using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Subcategory
{
    public class SubcategoryCreateView
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int CategoryId { get; set; }
    }
}