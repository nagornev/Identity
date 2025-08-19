using Auth.Domain.Aggregates;
using DDD.Specifications;
using System.Linq.Expressions;

namespace Auth.Domain.Specifications
{
    public class ScopeByIdsSpecification : Specification<Scope>
    {
        private readonly IEnumerable<Guid> _ids;

        public ScopeByIdsSpecification(IEnumerable<Guid> ids)
        {
            _ids = ids;
        }

        public override Expression<Func<Scope, bool>> ToExpression()
        {
            return x => _ids.Contains(x.Id);
        }
    }
}
