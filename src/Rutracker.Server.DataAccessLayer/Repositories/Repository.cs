using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Entities.Base;
using Rutracker.Server.DataAccessLayer.Interfaces;

namespace Rutracker.Server.DataAccessLayer.Repositories
{
    public class Repository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : BaseEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        protected readonly RutrackerContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(RutrackerContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public virtual async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return await _dbSet.SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.SingleOrDefaultAsync(expression);
        }

        public virtual async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.CountAsync(expression);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}