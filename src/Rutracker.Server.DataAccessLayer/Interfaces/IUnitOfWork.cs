using System.Threading.Tasks;

namespace Rutracker.Server.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}