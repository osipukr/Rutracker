using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Message
{
    public class MessageCreateViewModel
    {
        [Required] public string Text { get; set; }
        [Required] public int DialogId { get; set; }
    }
}