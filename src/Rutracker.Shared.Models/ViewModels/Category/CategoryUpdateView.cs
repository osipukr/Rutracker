using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Category
{
    public class CategoryUpdateView : View
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}