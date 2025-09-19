using OperationResults;

namespace Notification.Domain.Exceptions.Domains
{
    public class OutOfRangeDomainException : DomainException
    {
        public OutOfRangeDomainException(string message)
            : base(ResultErrorTypes.OutOfRange, message)
        {
        }
    }
}
