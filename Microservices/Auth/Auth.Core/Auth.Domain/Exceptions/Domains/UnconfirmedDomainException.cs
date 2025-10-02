using OperationResults;

namespace Auth.Domain.Exceptions.Domains
{
    public class UnconfirmedDomainException : DomainException
    {
        public UnconfirmedDomainException(string message)
            : base(ResultErrorTypes.Unconfirmed, message)
        {
        }
    }
}
