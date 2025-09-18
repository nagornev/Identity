using DDD.Specifications;
using Notification.Domain.Aggregates;
using System.Linq.Expressions;

namespace Notification.Domain.Specifications
{
    public class PendingNotificationMessageByIdSpecification : Specification<PendingNotificationMessage>
    {
        private readonly Guid _id;

        public PendingNotificationMessageByIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<PendingNotificationMessage, bool>> ToExpression()
        {
            return x => x.Id == _id;
        }
    }
}
