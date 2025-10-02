using DDD.Specifications;
using Notification.Domain.Aggregates;
using System.Linq.Expressions;

namespace Notification.Domain.Specifications
{
    public class PendingNotificationMessageByUserIdSpecification : Specification<PendingNotificationMessage>
    {
        private readonly Guid _userId;

        public PendingNotificationMessageByUserIdSpecification(Guid userId)
        {
            _userId = userId;
        }

        public override Expression<Func<PendingNotificationMessage, bool>> ToExpression()
        {
            return x => x.UserId == _userId;
        }
    }
}
