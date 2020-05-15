using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities.Base;

namespace Rutracker.Server.DataAccessLayer.Interfaces.Base
{
    public interface IRepository<TEntity> : IRepository where TEntity : Entity
    {
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression = null);

        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression = null);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null);

        Task<TEntity> AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}