using System;
using System.Linq.Expressions;
using Rutracker.Core.Entities;

namespace Rutracker.Core.Interfaces
{
    public interface ISpecification<TEntity, TPrimaryKey>
        where TEntity : BaseEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        Expression<Func<TEntity, bool>> Where { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}