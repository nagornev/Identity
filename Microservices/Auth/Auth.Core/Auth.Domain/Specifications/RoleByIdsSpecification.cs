using Auth.Domain.Aggregates;
using DDD.Specifications;
using System.Linq.Expressions;

namespace Auth.Domain.Specifications
{
    public class RoleByIdsSpecification : Specification<Role>
    {
        private readonly IEnumerable<Guid> _ids;

        public RoleByIdsSpecification(IEnumerable<Guid> ids)
        {
            _ids = ids;
        }

        public override Expression<Func<Role, bool>> ToExpression()
        {
            return x => _ids.Contains(x.Id);
        }
    }
}
