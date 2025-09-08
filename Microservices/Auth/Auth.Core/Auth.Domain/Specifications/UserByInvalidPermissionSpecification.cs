using Auth.Domain.Aggregates;
using DDD.Specifications;
using System.Linq.Expressions;

namespace Auth.Domain.Specifications
{
    public class UserByInvalidPermissionSpecification : Specification<User>
    {
        private readonly long _timestamp;

        public UserByInvalidPermissionSpecification(long timestamp)
        {
            _timestamp = timestamp;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return x => x.Authorization.RolePermissions.Any(rp => rp.Revoked) ||
                        x.Authorization.ScopePermissions.Any(sp => sp.Revoked || (sp.ExpiresAt.HasValue && sp.ExpiresAt < _timestamp));
        }
    }
}
