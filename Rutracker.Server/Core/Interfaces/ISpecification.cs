using System;
using System.Linq.Expressions;
using Rutracker.Server.Core.Entities;

namespace Rutracker.Server.Core.Interfaces
{
    public interface ISpecification<TEntity, TPrimaryKey>
        where TEntity : BaseEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        Expression<Func<TEntity, bool>> Where { get; }
        Expression<Func<TEntity, object>> OrderBy { get; }
        Expression<Func<TEntity, object>> OrderByDescending { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}