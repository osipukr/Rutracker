using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Subcategory
{
    public class SubcategoryUpdateViewModel
    {
        [Required] public string Name { get; set; }
    }
}