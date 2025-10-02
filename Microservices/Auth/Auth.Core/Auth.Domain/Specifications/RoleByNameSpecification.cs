using Auth.Domain.Aggregates;
using DDD.Specifications;
using System.Linq.Expressions;

namespace Auth.Domain.Specifications
{
    public class RoleByNameSpecification : Specification<Role>
    {
        private readonly string _name;

        public RoleByNameSpecification(string name)
        {
            _name = name;
        }

        public override Expression<Func<Role, bool>> ToExpression()
        {
            return (role) => role.Name == _name;
        }
    }
}
