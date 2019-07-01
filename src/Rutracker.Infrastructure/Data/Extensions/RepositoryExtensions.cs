using System;
using System.Linq;
using Rutracker.Core.Entities;
using Rutracker.Core.Interfaces;

namespace Rutracker.Infrastructure.Data.Extensions
{
    public static class RepositoryExtensions
    {
        public static IQueryable<TEntity> ApplySpecification<TEntity, TPrimaryKey>(this IQueryable<TEntity> query,
            ISpecification<TEntity, TPrimaryKey> specification)
            where TEntity : BaseEntity<TPrimaryKey>
            where TPrimaryKey : IEquatable<TPrimaryKey>
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            return SpecificationEvaluator<TEntity, TPrimaryKey>.Apply(query, specification);
        }
    }
}