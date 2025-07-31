using Auth.Domain.Aggregates;
using DDD.Specifications;
using System.Linq.Expressions;

namespace Auth.Domain.Specifications
{
    public class RoleNameSpecification : Specification<Role>
    {
        private readonly string _name;

        public RoleNameSpecification(string name)
        {
            _name = name;
        }

        public override Expression<Func<Role, bool>> ToExpression()
        {
            return (role) => role.Name == _name;
        }
    }
}
