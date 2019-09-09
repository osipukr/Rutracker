using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Category
{
    public class CategoryCreateViewModel
    {
        [Required] public string Name { get; set; }
    }
}