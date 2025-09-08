using Auth.Domain.Aggregates;
using DDD.Specifications;
using System.Linq.Expressions;

namespace Auth.Domain.Specifications
{
    public class ScopeByIdSpecification : Specification<Scope>
    {
        private readonly Guid _id;

        public ScopeByIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<Scope, bool>> ToExpression()
        {
            return x => x.Id == _id;
        }
    }
}
