using OperationResults;

namespace Auth.Domain.Exceptions.Domains
{
    public class DomainException : ResultException
    {
        public DomainException(int type, string message)
            : base(type, message)
        {
        }
    }
}
