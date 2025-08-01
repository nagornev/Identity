using Auth.Domain.Aggregates;
using DDD.Specifications;
using System.Linq.Expressions;

namespace Auth.Domain.Specifications
{
    public class SessionIdSpecification : Specification<Session>
    {
        private readonly Guid _sessionId;

        public SessionIdSpecification(Guid sessionId)
        {
            _sessionId = sessionId;
        }

        public override Expression<Func<Session, bool>> ToExpression()
        {
            return (session) => session.Id == _sessionId;
        }
    }
}
