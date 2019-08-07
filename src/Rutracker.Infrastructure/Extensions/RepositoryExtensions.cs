using System;
using System.Linq;
using Ardalis.GuardClauses;
using Rutracker.Core.Entities.Torrents;
using Rutracker.Core.Interfaces.Specifications;
using Rutracker.Infrastructure.Data.Specifications;

namespace Rutracker.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static IQueryable<TEntity> ApplySpecification<TEntity, TPrimaryKey>(this IQueryable<TEntity> query,
            ISpecification<TEntity, TPrimaryKey> specification)
            where TEntity : BaseEntity<TPrimaryKey>
            where TPrimaryKey : IEquatable<TPrimaryKey>
        {
            Guard.Against.Null(query, nameof(query));
            Guard.Against.Null(specification, nameof(specification));

            return SpecificationEvaluator<TEntity, TPrimaryKey>.Apply(query, specification);
        }
    }
}