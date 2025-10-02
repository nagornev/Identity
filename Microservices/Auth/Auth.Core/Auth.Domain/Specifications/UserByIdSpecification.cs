using Auth.Domain.Aggregates;
using DDD.Specifications;
using System.Linq.Expressions;

namespace Auth.Domain.Specifications
{
    public class UserByIdSpecification : Specification<User>
    {
        private readonly Guid _userId;

        public UserByIdSpecification(Guid userId)
        {
            _userId = userId;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return (user) => user.Id == _userId;
        }
    }
}
