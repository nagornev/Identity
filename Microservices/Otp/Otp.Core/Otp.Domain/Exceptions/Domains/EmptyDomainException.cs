using OperationResults;

namespace Otp.Domain.Exceptions.Domains
{
    public class EmptyDomainException : DomainException
    {
        public EmptyDomainException(string message)
            : base(ResultErrorTypes.Empty, message)
        {
        }
    }
}
