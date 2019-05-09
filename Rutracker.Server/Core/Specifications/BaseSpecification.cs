using System;
using System.Linq.Expressions;
using Rutracker.Server.Core.Entities;
using Rutracker.Server.Core.Interfaces;

namespace Rutracker.Server.Core.Specifications
{
    public abstract class BaseSpecification<TEntity, TPrimaryKey> : ISpecification<TEntity, TPrimaryKey>
        where TEntity : BaseEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        public Expression<Func<TEntity, bool>> Where { get; private set; }
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }

        protected BaseSpecification(Expression<Func<TEntity, bool>> expression) =>
            Where = expression;

        protected virtual void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        protected virtual void ApplyOrderBy(Expression<Func<TEntity, object>> expression) =>
            OrderBy = expression;

        protected virtual void ApplyOrderByDescending(Expression<Func<TEntity, object>> expression) =>
            OrderByDescending = expression;
    }
}