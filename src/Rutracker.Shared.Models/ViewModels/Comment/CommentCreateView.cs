using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Comment
{
    public class CommentCreateView
    {
        [Required]
        public string Text { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int TorrentId { get; set; }
    }
}