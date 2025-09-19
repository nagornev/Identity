using OperationResults;

namespace Auth.Domain.Exceptions.Domains
{
    public class EmptyDomainException : DomainException
    {
        public EmptyDomainException(string message)
            : base(ResultErrorTypes.Empty, message)
        {
        }
    }
}
