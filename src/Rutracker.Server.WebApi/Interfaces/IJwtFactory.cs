using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.WebApi.Interfaces
{
    public interface IJwtFactory
    {
        Task<string> GenerateTokenAsync(User user, IEnumerable<Role> roles);
    }
}