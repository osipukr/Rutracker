using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.Account;

namespace Rutracker.Server.WebApi.Interfaces
{
    public interface IJwtService
    {
        Task<TokenView> GenerateTokenAsync(User user, IEnumerable<Role> roles);
    }
}