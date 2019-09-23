using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Category
{
    public class CategoryUpdateViewModel
    {
        [Required, MaxLength(100)] public string Name { get; set; }
    }
}