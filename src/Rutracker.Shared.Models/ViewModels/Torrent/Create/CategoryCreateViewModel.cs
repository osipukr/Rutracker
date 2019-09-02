using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Torrent.Create
{
    public class CategoryCreateViewModel
    {
        [Required] public string Name { get; set; }
    }
}