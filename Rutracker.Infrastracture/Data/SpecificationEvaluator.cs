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

            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }

            return query.AsNoTracking();
        }
    }
}