using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Comment
{
    public class CommentUpdateViewModel
    {
        [Required] public string Text { get; set; }
    }
}