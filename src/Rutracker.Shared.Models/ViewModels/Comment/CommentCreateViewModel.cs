using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Comment
{
    public class CommentCreateViewModel
    {
        [Required]
        [MaxLength(300)]
        public string Text { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int TorrentId { get; set; }
    }
}