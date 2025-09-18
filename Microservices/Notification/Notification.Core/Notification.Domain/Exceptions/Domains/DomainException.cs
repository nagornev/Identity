using OperationResults;

namespace Notification.Domain.Exceptions.Domains
{
    public class DomainException : ResultException
    {
        public DomainException(int type, string message)
            : base(type, message)
        {
        }
    }
}
