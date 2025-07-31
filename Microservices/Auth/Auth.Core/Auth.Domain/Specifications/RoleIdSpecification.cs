using Auth.Domain.Aggregates;
using DDD.Specifications;
using System.Linq.Expressions;

namespace Auth.Domain.Specifications
{
    public class RoleIdSpecification : Specification<Role>
    {
        private readonly Guid _roleId;

        public RoleIdSpecification(Guid roleId)
        {
            _roleId = roleId;
        }

        public override Expression<Func<Role, bool>> ToExpression()
        {
            return (role) => role.Id == _roleId;
        }
    }
}
