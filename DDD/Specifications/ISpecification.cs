using DDD.Primitives;
using System.Linq.Expressions;

namespace DDD.Specifications
{
    public interface ISpecification<TAggragateType>
        where TAggragateType : AggregateRoot
    {
        Expression<Func<TAggragateType, bool>> ToExpression();
    }
}
