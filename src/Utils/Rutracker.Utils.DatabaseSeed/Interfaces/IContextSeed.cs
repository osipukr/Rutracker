using System.Threading.Tasks;

namespace Rutracker.Utils.DatabaseSeed.Interfaces
{
    public interface IContextSeed
    {
        Task SeedAsync();
    }
}