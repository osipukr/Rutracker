using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rutracker.Core.Entities;
using Rutracker.Core.Interfaces;
using Rutracker.Infrastructure.Data.Extensions;

namespace Rutracker.Infrastructure.Data
{
    public abstract class Repository<TEntity, TPrimaryKey> : BaseRepository, IRepository<TEntity, TPrimaryKey>
        where TEntity : BaseEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        protected readonly DbSet<TEntity> _dbSet;

        protected Repository(TorrentContext context)
            : base(context)
        {
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetAsync(TPrimaryKey id) =>
            await _dbSet.SingleOrDefaultAsync(x => x.Id.Equals(id));

        public virtual async Task<TEntity> GetAsync(ISpecification<TEntity, TPrimaryKey> specification) =>
            await _dbSet.ApplySpecification(specification).SingleOrDefaultAsync();

        public virtual async Task<IReadOnlyList<TEntity>> ListAsync() => 
            await _dbSet.ToArrayAsync();

        public virtual async Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity, TPrimaryKey> specification) =>
            await _dbSet.ApplySpecification(specification).ToArrayAsync();

        public virtual async Task<int> CountAsync() => 
            await _dbSet.CountAsync();

        public virtual async Task<int> CountAsync(ISpecification<TEntity, TPrimaryKey> specification) =>
            await _dbSet.ApplySpecification(specification).CountAsync();
    }
}