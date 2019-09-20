using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Subcategory
{
    public class SubcategoryUpdateViewModel
    {
        [Required, MaxLength(100)] public string Name { get; set; }
    }
}