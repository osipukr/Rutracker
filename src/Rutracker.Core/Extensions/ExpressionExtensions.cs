using System;
using System.Linq.Expressions;

namespace Rutracker.Core.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exp,
            Expression<Func<T, bool>> expTwo)
        {
            if (expTwo == null)
            {
                throw new ArgumentNullException(nameof(expTwo));
            }

            var visit = ApplyVisit(exp, expTwo);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(visit, expTwo.Body), expTwo.Parameters);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exp,
            Expression<Func<T, bool>> expTwo)
        {
            if (expTwo == null)
            {
                throw new ArgumentNullException(nameof(expTwo));
            }

            var visit = ApplyVisit(exp, expTwo);

            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(visit, expTwo.Body), expTwo.Parameters);
        }

        private static Expression ApplyVisit<T>(Expression<Func<T, bool>> fromExpression,
            Expression<Func<T, bool>> toExpression)
        {
            var visit = new SwapVisitor(fromExpression.Parameters[0],
                toExpression.Parameters[0]).Visit(fromExpression.Body);

            if (visit == null)
            {
                throw new ArgumentException(string.Empty, nameof(visit));
            }

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