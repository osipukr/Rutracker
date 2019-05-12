using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Core.Entities;

namespace Rutracker.Core.Interfaces
{
    public interface IRepository<TEntity, TPrimaryKey> 
        where TEntity : BaseEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        Task<TEntity> GetAsync(TPrimaryKey id);
        Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity, TPrimaryKey> specification);
        Task<int> CountAsync(ISpecification<TEntity, TPrimaryKey> specification);
    }
}