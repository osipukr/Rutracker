using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Torrent.Update
{
    public class CommentUpdateViewModel
    {
        [Required] public string Text { get; set; }
    }
}