using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Rutracker.Core.Entities;
using Rutracker.Core.Interfaces;

namespace Rutracker.Infrastructure.Data
{
    public static class SpecificationEvaluator<TEntity, TPrimaryKey>
        where TEntity : BaseEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        public static IQueryable<TEntity> ApplySpecification(IQueryable<TEntity> src, ISpecification<TEntity, TPrimaryKey> specification)
        {
            var query = src;

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