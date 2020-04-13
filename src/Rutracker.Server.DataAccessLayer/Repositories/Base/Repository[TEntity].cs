using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.DataAccessLayer.Entities.Base;
using Rutracker.Server.DataAccessLayer.Interfaces.Base;

namespace Rutracker.Server.DataAccessLayer.Repositories.Base
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected Repository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _dbSet = _context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
            {
                return _dbSet;
            }

            return _dbSet.Where(expression);
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
            {
                return await _dbSet.FirstOrDefaultAsync();
            }

            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public virtual async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
            {
                return await _dbSet.AnyAsync();
            }

            return await _dbSet.AnyAsync(expression);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
            {
                return await _dbSet.CountAsync();
            }

            return await _dbSet.CountAsync(expression);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            return (await _dbSet.AddAsync(entity)).Entity;
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }
}