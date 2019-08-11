using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Rutracker.Core.Entities.Identity;

namespace Rutracker.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<IReadOnlyList<User>> GetAllUserAsync();
        Task<User> GetUserAsync(ClaimsPrincipal principal);
        Task UpdateUserAsync(User user);
    }
}