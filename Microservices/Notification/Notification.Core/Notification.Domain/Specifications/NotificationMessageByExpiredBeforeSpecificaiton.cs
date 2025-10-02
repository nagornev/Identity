using DDD.Specifications;
using Notification.Domain.Aggregates;
using System.Linq.Expressions;

namespace Notification.Domain.Specifications
{
    public class NotificationMessageByExpiredBeforeSpecificaiton : Specification<NotificationMessage>
    {
        private readonly long _timestamp;

        public NotificationMessageByExpiredBeforeSpecificaiton(long timestamp)
        {
            _timestamp = timestamp;
        }

        public override Expression<Func<NotificationMessage, bool>> ToExpression()
        {
            return x => x.ExpiresAt < _timestamp;
        }
    }
}
