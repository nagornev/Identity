using Auth.Domain.Aggregates;
using DDD.Specifications;
using System.Linq.Expressions;

namespace Auth.Domain.Specifications
{
    public class SessionByInvalidParametersSpecification : Specification<Session>
    {
        private readonly long _timestamp;

        private readonly int _unactiveWindow;

        public SessionByInvalidParametersSpecification(long timestamp, int unactiveWindow)
        {
            _timestamp = timestamp;
            _unactiveWindow = unactiveWindow;
        }

        public override Expression<Func<Session, bool>> ToExpression()
        {
            return x => x.Closed ||
                        x.Revoked ||
                        x.ExpiresAt > _timestamp ||
                        (!x.Activated && (_timestamp - x.CreatedAt) > _unactiveWindow);
        }
    }
}
