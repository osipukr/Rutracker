using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class ChangeUserViewModel
    {
        [StringLength(100)] public string FirstName { get; set; }
        [StringLength(100)] public string LastName { get; set; }
    }
}