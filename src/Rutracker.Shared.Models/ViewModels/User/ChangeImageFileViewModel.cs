using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class ChangeImageFileViewModel
    {
        [Required] public IFormFile File { get; set; }
    }
}