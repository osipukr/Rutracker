using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Shared.ViewModels.Accounts;
using Rutracker.Shared.ViewModels.Users;

namespace Rutracker.Client.Services.Interfaces
{
    public interface IUserService
    {
        Task<IReadOnlyList<UserViewModel>> Users(JwtToken token);
        Task<UserResponseViewModel> UserDetails(JwtToken token);
    }
}