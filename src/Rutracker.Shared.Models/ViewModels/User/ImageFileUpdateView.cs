using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class ImageFileUpdateView
    {
        [Required] 
        public IFormFile File { get; set; }
    }
}