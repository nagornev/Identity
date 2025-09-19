using OperationResults;

namespace Auth.Domain.Exceptions.Domains
{
    public class AlreadyDomainException : DomainException
    {
        public AlreadyDomainException(string message)
            : base(ResultErrorTypes.Already, message)
        {
        }
    }
}
