using Auth.Domain.Aggregates;
using DDD.Specifications;
using System.Linq.Expressions;

namespace Auth.Domain.Specifications
{
    public class SessionUserIdSpecification : Specification<Session>
    {
        private readonly Guid _userId;

        public SessionUserIdSpecification(Guid userId)
        {
            _userId = userId;
        }

        public override Expression<Func<Session, bool>> ToExpression()
        {
            return (session) => session.UserId == _userId;
        }
    }
}
