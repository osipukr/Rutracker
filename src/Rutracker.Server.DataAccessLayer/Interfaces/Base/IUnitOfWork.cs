using System;
using System.Threading.Tasks;

namespace Rutracker.Server.DataAccessLayer.Interfaces.Base
{
    public interface IUnitOfWork : IRepositoryFactory, IDisposable
    {
        Task<int> SaveChangesAsync();
    }
}