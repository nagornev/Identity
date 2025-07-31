using DDD.Primitives;
using System.Linq.Expressions;

namespace DDD.Specifications
{
    public abstract class Specification<T> : ISpecification<T>
        where T : AggregateRoot
    {
        public abstract Expression<Func<T, bool>> ToExpression();

        public Func<T, bool> ToPredicate() => ToExpression().Compile();

        public bool IsSatisfiedBy(T entity) => ToPredicate()(entity);

        public Specification<T> And(ISpecification<T> other)
            => new AndSpecification<T>(this, other);

        public Specification<T> Or(ISpecification<T> other)
            => new OrSpecification<T>(this, other);

        public Specification<T> Not()
            => new NotSpecification<T>(this);
    }
}
