using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rutracker.Core.Entities;
using Rutracker.Core.Interfaces;

namespace Rutracker.Infrastructure.Data
{
    public abstract class Repository<TEntity, TPrimaryKey> : BaseRepository, IRepository<TEntity, TPrimaryKey>
        where TEntity : BaseEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        protected Repository(TorrentContext context)
            : base(context)
        {
        }

        public virtual async Task<TEntity> GetAsync(TPrimaryKey id) =>
            await _context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id.Equals(id));

        public async Task<IReadOnlyList<TEntity>> ListAsync() => 
            await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        public virtual async Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity, TPrimaryKey> specification) =>
            await ApplySpecification(specification).ToListAsync();

        public async Task<int> CountAsync() =>
            await _context.Set<TEntity>().CountAsync();

        public virtual async Task<int> CountAsync(ISpecification<TEntity, TPrimaryKey> specification) =>
            await _context.Set<TEntity>().CountAsync(specification.Where);

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity, TPrimaryKey> specification) =>
           SpecificationEvaluator<TEntity, TPrimaryKey>.ApplySpecification(_context.Set<TEntity>(), specification);
    }
}