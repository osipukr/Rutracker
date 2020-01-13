using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities.Base;

namespace Rutracker.Server.DataAccessLayer.Interfaces
{
    public interface IRepository<TEntity, in TPrimaryKey>
        where TEntity : BaseEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetAsync(TPrimaryKey id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<bool> ExistAsync(TPrimaryKey id);
        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}