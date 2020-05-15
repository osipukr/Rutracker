using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.DataAccessLayer.Entities.Base;
using Rutracker.Server.DataAccessLayer.Interfaces.Base;

namespace Rutracker.Server.DataAccessLayer.Repositories.Base
{
    public abstract class Repository<TEntity, TPrimaryKey> : Repository<TEntity>, IRepository<TEntity, TPrimaryKey>
        where TEntity : Entity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        protected Repository(DbContext context) : base(context)
        {
        }

        public virtual async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return await GetAsync(entity => entity.Id.Equals(id));
        }

        public virtual async Task<bool> ExistAsync(TPrimaryKey id)
        {
            return await ExistAsync(entity => entity.Id.Equals(id));
        }
    }
}