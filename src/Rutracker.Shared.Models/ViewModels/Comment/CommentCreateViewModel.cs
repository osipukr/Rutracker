using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Comment
{
    public class CommentCreateViewModel
    {
        [Required] public string Text { get; set; }
        [Required] public int TorrentId { get; set; }
    }
}