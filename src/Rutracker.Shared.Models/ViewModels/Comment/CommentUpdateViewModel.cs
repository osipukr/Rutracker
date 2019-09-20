using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Comment
{
    public class CommentUpdateViewModel
    {
        [Required, MaxLength(300)] public string Text { get; set; }
    }
}