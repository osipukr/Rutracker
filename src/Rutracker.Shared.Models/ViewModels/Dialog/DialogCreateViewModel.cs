using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Dialog
{
    public class DialogCreateViewModel
    {
        [Required] public string Title { get; set; }
        [Required] public IEnumerable<string> UserIds { get; set; }
    }
}