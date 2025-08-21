using Auth.Domain.Aggregates;
using DDD.Specifications;
using System.Linq.Expressions;

namespace Auth.Domain.Specifications
{
    public class UserCreatedBeforeSpecification : Specification<User>
    {
        private readonly long _timestamp;

        public UserCreatedBeforeSpecification(long timestamp)
        {
            _timestamp = timestamp;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return x => x.CreatedAt < _timestamp;
        }
    }
}
