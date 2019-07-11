using System;
using System.Linq.Expressions;
using Ardalis.GuardClauses;

namespace Rutracker.Core.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expression,
            Expression<Func<T, bool>> otherExpression)
        {
            Guard.Against.Null(otherExpression, nameof(otherExpression));

            var visit = ApplyVisit(expression, otherExpression);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(visit,
                    otherExpression.Body),
                otherExpression.Parameters);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expression,
            Expression<Func<T, bool>> otherExpression)
        {
            Guard.Against.Null(otherExpression, nameof(otherExpression));

            var visit = ApplyVisit(expression, otherExpression);

            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(visit,
                    otherExpression.Body),
                otherExpression.Parameters);
        }

        private static Expression ApplyVisit<T>(Expression<Func<T, bool>> fromExpression,
            Expression<Func<T, bool>> toExpression)
        {
            var visit = new SwapVisitor(fromExpression.Parameters[0],
                toExpression.Parameters[0]).Visit(fromExpression.Body);

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