using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rutracker.Core.Entities;
using Rutracker.Core.Interfaces.Repositories;
using Rutracker.Core.Interfaces.Specifications;
using Rutracker.Infrastructure.Data.Contexts;
using Rutracker.Infrastructure.Extensions;

namespace Rutracker.Infrastructure.Data.Repositories
{
    public abstract class Repository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : BaseEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        protected readonly TorrentContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected Repository(TorrentContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetAsync(TPrimaryKey id) =>
            await _dbSet.SingleOrDefaultAsync(x => x.Id.Equals(id));

        public virtual async Task<TEntity> GetAsync(ISpecification<TEntity, TPrimaryKey> specification) =>
            await _dbSet.ApplySpecification(specification).SingleOrDefaultAsync();

        public virtual async Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity, TPrimaryKey> specification) =>
            await _dbSet.ApplySpecification(specification).ToListAsync();

        public virtual async Task<int> CountAsync(ISpecification<TEntity, TPrimaryKey> specification) =>
            await _dbSet.ApplySpecification(specification).CountAsync();
    }
}