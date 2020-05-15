using System.Threading.Tasks;
using Rutracker.Shared.Infrastructure.Collections;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Client.Infrastructure.Interfaces
{
    public interface IUserService
    {
        Task<PagedList<UserView>> ListAsync(UserFilter filter);
        Task<UserView> FindAsync(string userName);
        Task<UserDetailView> FindAsync();
        Task<UserDetailView> UpdateAsync(UserUpdateView model);
        Task<UserDetailView> ChangePasswordAsync(PasswordUpdateView model);
    }
}