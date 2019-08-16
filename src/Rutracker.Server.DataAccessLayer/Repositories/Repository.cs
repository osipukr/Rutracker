using System;
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
        protected readonly TorrentContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(TorrentContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll() => _dbSet;

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression) => _dbSet.Where(expression);

        public virtual async Task<TEntity> GetAsync(TPrimaryKey id) => await _dbSet.SingleOrDefaultAsync(x => x.Id.Equals(id));

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression) => await _dbSet.SingleOrDefaultAsync(expression);

        public virtual async Task<int> CountAsync() => await _dbSet.CountAsync();

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression) => await _dbSet.CountAsync(expression);
    }
}