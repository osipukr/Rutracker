using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Category
{
    public class CategoryUpdateViewModel
    {
        [Required] public string Name { get; set; }
    }
}