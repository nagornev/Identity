using DDD.Specifications;
using Notification.Domain.Aggregates;
using System.Linq.Expressions;

namespace Notification.Domain.Specifications
{
    public class PendingNotificationMessageByExpiredBeforeSpecification : Specification<PendingNotificationMessage>
    {
        private readonly long _timestamp;

        public PendingNotificationMessageByExpiredBeforeSpecification(long timestamp)
        {
            _timestamp = timestamp;
        }

        public override Expression<Func<PendingNotificationMessage, bool>> ToExpression()
        {
            return x => x.ExpiresAt < _timestamp;
        }
    }
}
