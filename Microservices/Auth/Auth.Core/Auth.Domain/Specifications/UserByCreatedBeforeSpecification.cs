using Auth.Domain.Aggregates;
using DDD.Specifications;
using System.Linq.Expressions;

namespace Auth.Domain.Specifications
{
    public class UserByCreatedBeforeSpecification : Specification<User>
    {
        private readonly long _timestamp;

        public UserByCreatedBeforeSpecification(long timestamp)
        {
            _timestamp = timestamp;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return x => x.CreatedAt < _timestamp;
        }
    }
}
