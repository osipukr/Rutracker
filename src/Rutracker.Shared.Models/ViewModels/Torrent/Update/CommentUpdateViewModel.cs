using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Torrent.Update
{
    public class CommentUpdateViewModel
    {
        [Required] public int Id { get; set; }
        [Required] public string Text { get; set; }
    }
}