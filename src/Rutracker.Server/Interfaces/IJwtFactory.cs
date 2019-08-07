using System.Threading.Tasks;
using Rutracker.Core.Entities.Accounts;
using Rutracker.Shared.ViewModels.Accounts;

namespace Rutracker.Server.Interfaces
{
    public interface IJwtFactory
    {
        Task<JwtToken> GenerateTokenAsync(User user);
    }
}