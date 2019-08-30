using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Torrent.Create
{
    public class LikeCreateViewModel
    {
        [Required] public int CommentId { get; set; }
    }
}