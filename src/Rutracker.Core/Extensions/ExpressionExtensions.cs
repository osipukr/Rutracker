using System;
using System.Linq.Expressions;
using Ardalis.GuardClauses;

namespace Rutracker.Core.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var visit = ApplyVisit(left, right);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(visit, right.Body), right.Parameters);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var visit = ApplyVisit(left, right);

            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(visit, right.Body), right.Parameters);
        }

        private static Expression ApplyVisit<T>(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            Guard.Against.Null(left, nameof(left));
            Guard.Against.Null(right, nameof(right));

            var visit = new SwapVisitor(left.Parameters[0], right.Parameters[0]).Visit(left.Body);

            Guard.Against.Null(visit, nameof(visit));

            return visit;
        }

        private class SwapVisitor : ExpressionVisitor
        {
            private readonly Expression _from;
            private readonly Expression _to;

            public SwapVisitor(Expression from, Expression to)
            {
                _from = from;
                _to = to;
            }

            public override Expression Visit(Expression node) => node == _from ? _to : base.Visit(node);
        }
    }
}