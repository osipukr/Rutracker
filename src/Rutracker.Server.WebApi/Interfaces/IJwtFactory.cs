using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models;

namespace Rutracker.Server.WebApi.Interfaces
{
    public interface IJwtFactory
    {
        Task<JwtToken> GenerateTokenAsync(User user, IEnumerable<string> roles);
    }
}