using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Comment
{
    public class CommentUpdateView : View
    {
        [Required]
        public string Text { get; set; }
    }
}