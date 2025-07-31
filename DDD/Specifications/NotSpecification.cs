using DDD.Primitives;
using System.Linq.Expressions;

namespace DDD.Specifications
{
    internal class NotSpecification<T> : Specification<T>
        where T : AggregateRoot
    {
        private readonly Specification<T> _inner;

        public NotSpecification(Specification<T> inner)
        {
            _inner = inner;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var innerExpr = _inner.ToExpression();
            var parameter = innerExpr.Parameters[0];
            var body = Expression.Not(innerExpr.Body);
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}