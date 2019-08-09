using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Rutracker.Shared.ViewModels.Users;

namespace Rutracker.Server.Interfaces
{
    public interface IUserViewModelService
    {
        Task<IReadOnlyList<UserViewModel>> GetUsersAsync();
        Task<UserResponseViewModel> GetUserAsync(ClaimsPrincipal principal);
    }
}