using DDD.Primitives;
using System.Linq.Expressions;

namespace DDD.Specifications
{
    internal class AndSpecification<T> : Specification<T>
        where T : AggregateRoot
    {
        private readonly ISpecification<T> _left;

        private readonly ISpecification<T> _right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var left = _left.ToExpression();
            var right = _right.ToExpression();

            var parameter = Expression.Parameter(typeof(T));
            var combined = Expression.AndAlso(
                Expression.Invoke(left, parameter),
                Expression.Invoke(right, parameter)
            );

            return Expression.Lambda<Func<T, bool>>(combined, parameter);
        }
    }
}