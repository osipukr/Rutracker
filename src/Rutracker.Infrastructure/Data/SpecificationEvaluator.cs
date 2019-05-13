using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Rutracker.Core.Entities;
using Rutracker.Core.Interfaces;

namespace Rutracker.Infrastructure.Data
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity, TPrimaryKey>(this IQueryable<TEntity> inputQuery,
            ISpecification<TEntity, TPrimaryKey> specification)
            where TEntity : BaseEntity<TPrimaryKey>
            where TPrimaryKey : IEquatable<TPrimaryKey>
        {
            var query = inputQuery;

            if (specification.Where != null)
            {
                query = query.Where(specification.Where);
            }

            query = query.OrderBy(x => x.Id);

            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }

            return query.AsNoTracking();
        }
    }
}