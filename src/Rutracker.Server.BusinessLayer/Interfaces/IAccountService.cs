using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IAccountService
    {
        Task<User> CreateAsync(string userName, string email, string password);
        Task<User> LoginAsync(string userName, string password, bool rememberMe);
        Task LogoutAsync();
    }
}