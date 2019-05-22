using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rutracker.Core.Entities;
using Rutracker.Core.Interfaces;

namespace Rutracker.Infrastructure.Data
{
    public class Repository<TEntity, TPrimaryKey> : BaseRepository, IRepository<TEntity, TPrimaryKey>
        where TEntity : BaseEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        protected Repository(TorrentContext context)
            : base(context)
        {
        }

        public async Task<TEntity> GetAsync(TPrimaryKey id) => await _context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id.Equals(id));

        public virtual async Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity, TPrimaryKey> specification) =>
            await ApplySpecification(specification).ToListAsync();

        public virtual async Task<int> CountAsync(ISpecification<TEntity, TPrimaryKey> specification) =>
            await _context.Set<TEntity>().CountAsync(specification.Where);

        protected IQueryable<TEntity> ApplySpecification(ISpecification<TEntity, TPrimaryKey> specification) =>
            _context.Set<TEntity>().GetQuery(specification);
    }
}