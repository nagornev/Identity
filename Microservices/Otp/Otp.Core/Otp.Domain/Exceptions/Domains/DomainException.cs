using OperationResults;

namespace Otp.Domain.Exceptions.Domains
{
    public abstract class DomainException : ResultException
    {
        public DomainException(int type, string message)
            : base(type, message)
        {
        }
    }
}
