using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Subcategory
{
    public class SubcategoryCreateViewModel
    {
        [Required] public string Name { get; set; }
        [Required] public int CategoryId { get; set; }
    }
}