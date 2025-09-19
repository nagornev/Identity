using OperationResults;

namespace Otp.Domain.Exceptions.Domains
{
    public abstract class NullDomainException : DomainException
    {
        public NullDomainException(string message)
            : base(ResultErrorTypes.Null, message)
        {
        }
    }
}
