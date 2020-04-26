using System.Threading.Tasks;
using Rutracker.Shared.Infrastructure.Collections;
using Rutracker.Shared.Infrastructure.Filters;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Client.Infrastructure.Interfaces
{
    public interface IUserService
    {
        Task<IPagedList<UserView>> ListAsync(IUserFilter filter);
        Task<UserView> FindAsync(string userName);
        Task<UserDetailView> FindAsync();
        Task<UserDetailView> UpdateAsync(UserUpdateView model);
        Task<UserDetailView> ChangePasswordAsync(PasswordUpdateView model);
    }
}