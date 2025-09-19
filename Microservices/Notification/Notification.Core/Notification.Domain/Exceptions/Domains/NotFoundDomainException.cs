using OperationResults;

namespace Notification.Domain.Exceptions.Domains
{
    public class NotFoundDomainException : DomainException
    {
        public NotFoundDomainException(string message)
            : base(ResultErrorTypes.NotFound, message)
        {
        }
    }
}
