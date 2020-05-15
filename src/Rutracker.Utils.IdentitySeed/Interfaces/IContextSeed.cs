using System.Threading.Tasks;

namespace Rutracker.Utils.IdentitySeed.Interfaces
{
    public interface IContextSeed
    {
        Task SeedAsync();
    }
}