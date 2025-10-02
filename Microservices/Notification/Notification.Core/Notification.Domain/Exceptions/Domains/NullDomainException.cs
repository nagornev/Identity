using OperationResults;

namespace Notification.Domain.Exceptions.Domains
{
    public class NullDomainException : DomainException
    {
        public NullDomainException(string message)
            : base(ResultErrorTypes.Null, message)
        {
        }
    }
}
