using OperationResults;

namespace Otp.Domain.Exceptions.Domains
{
    public abstract class AlreadyDomainEvent : DomainException
    {
        public AlreadyDomainEvent(string message)
            : base(ResultErrorTypes.Already, message)
        {
        }
    }
}
