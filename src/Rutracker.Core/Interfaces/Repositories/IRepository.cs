using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Core.Entities;
using Rutracker.Core.Interfaces.Specifications;

namespace Rutracker.Core.Interfaces.Repositories
{
    public interface IRepository<TEntity, TPrimaryKey> 
        where TEntity : BaseEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        Task<TEntity> GetAsync(TPrimaryKey id);
        Task<TEntity> GetAsync(ISpecification<TEntity, TPrimaryKey> specification);
        Task<IReadOnlyList<TEntity>> ListAsync();
        Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity, TPrimaryKey> specification);
        Task<int> CountAsync();
        Task<int> CountAsync(ISpecification<TEntity, TPrimaryKey> specification);
    }
}