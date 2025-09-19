using OperationResults;

namespace Notification.Domain.Exceptions.Domains
{
    public class EmptyDomainException : DomainException
    {
        public EmptyDomainException(string message)
            : base(ResultErrorTypes.Empty, message)
        {
        }
    }
}
