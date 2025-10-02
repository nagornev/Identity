using Auth.Domain.Aggregates;
using DDD.Specifications;
using System.Linq.Expressions;

namespace Auth.Domain.Specifications
{
    public class UserByActiveSpecification : Specification<User>
    {
        private readonly bool _isActive;

        public UserByActiveSpecification(bool isActive)
        {
            _isActive = isActive;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return x => x.Activated == _isActive;
        }
    }
}
