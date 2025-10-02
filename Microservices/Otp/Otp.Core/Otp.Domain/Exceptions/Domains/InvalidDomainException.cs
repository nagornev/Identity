using OperationResults;

namespace Otp.Domain.Exceptions.Domains
{
    public abstract class InvalidDomainException : DomainException
    {
        public InvalidDomainException(string message)
            : base(ResultErrorTypes.Invalid, message)
        {
        }
    }
}
