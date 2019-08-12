using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<IReadOnlyList<User>> GetAllUserAsync();
        Task<User> GetUserAsync(ClaimsPrincipal principal);
        Task UpdateUserAsync(User user);
    }
}