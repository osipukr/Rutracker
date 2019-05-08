using System;
using System.Collections.Generic;
using Rutracker.Core.Entities;
using System.Threading.Tasks;

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