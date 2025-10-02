using DDD.Primitives;
using System.Linq.Expressions;

namespace DDD.Specifications
{
    internal class OrSpecification<T> : Specification<T>
        where T : AggregateRoot
    {
        private readonly ISpecification<T> _left;

        private readonly ISpecification<T> _right;

        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpr = _left.ToExpression();
            var rightExpr = _right.ToExpression();

            // Переиспользуем один параметр
            var parameter = Expression.Parameter(typeof(T), "x");

            var leftBody = ReplaceParameter(leftExpr.Body, leftExpr.Parameters[0], parameter);
            var rightBody = ReplaceParameter(rightExpr.Body, rightExpr.Parameters[0], parameter);

            var body = Expression.OrElse(leftBody, rightBody);
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        private static Expression ReplaceParameter(Expression expression, ParameterExpression source, ParameterExpression target)
        {
            return new ParameterReplacer(source, target).Visit(expression)!;
        }
    }
}