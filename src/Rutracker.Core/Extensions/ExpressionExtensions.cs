using System;
using System.Linq;
using System.Linq.Expressions;

namespace Rutracker.Core.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exp, Expression<Func<T, bool>> newExp)
        {
            // get the visitor
            var visitor = new ParameterUpdateVisitor(newExp.Parameters.First(), exp.Parameters.First());

            // replace the parameter in the expression just created
            newExp = visitor.Visit(newExp) as Expression<Func<T, bool>>;

            // now you can and together the two expressions
            // and return a new lambda, that will do what you want. NOTE that the binExp has
            // reference only to te newExp.Parameters[0] (there is only 1) parameter, and no other
            return Expression.Lambda<Func<T, bool>>(Expression.And(exp.Body, newExp.Body), newExp.Parameters);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exp, Expression<Func<T, bool>> newExp)
        {
            var visitor = new ParameterUpdateVisitor(newExp.Parameters.First(), exp.Parameters.First());

            newExp = visitor.Visit(newExp) as Expression<Func<T, bool>>;

            return Expression.Lambda<Func<T, bool>>(Expression.Or(exp.Body, newExp.Body), newExp.Parameters);
        }

        private class ParameterUpdateVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _oldParameter;
            private readonly ParameterExpression _newParameter;

            public ParameterUpdateVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
            {
                _oldParameter = oldParameter;
                _newParameter = newParameter;
            }

            protected override Expression VisitParameter(ParameterExpression node) =>
                ReferenceEquals(node,
                    _oldParameter)
                    ? _newParameter
                    : base.VisitParameter(node);
        }
    }
}