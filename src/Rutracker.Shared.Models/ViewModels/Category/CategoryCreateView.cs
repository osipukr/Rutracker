using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Category
{
    public class CategoryCreateView : View
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}