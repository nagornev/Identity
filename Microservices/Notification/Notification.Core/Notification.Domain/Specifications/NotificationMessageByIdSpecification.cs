using DDD.Specifications;
using Notification.Domain.Aggregates;
using System.Linq.Expressions;

namespace Notification.Domain.Specifications
{
    public class NotificationMessageByIdSpecification : Specification<NotificationMessage>
    {
        private readonly Guid _id;

        public NotificationMessageByIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<NotificationMessage, bool>> ToExpression()
        {
            return x => x.Id == _id;
        }
    }
}
