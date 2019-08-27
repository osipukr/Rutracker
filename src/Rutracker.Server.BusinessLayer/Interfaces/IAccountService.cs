using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IAccountService
    {
        Task<User> LoginAsync(string userName, string password, bool rememberMe);
        Task<User> RegisterAsync(string userName, string email, string password);
        Task LogoutAsync();
    }
}