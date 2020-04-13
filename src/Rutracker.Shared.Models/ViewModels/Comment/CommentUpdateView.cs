using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Comment
{
    public class CommentUpdateView
    {
        [Required]
        public string Text { get; set; }
    }
}