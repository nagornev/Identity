using OperationResults;

namespace Auth.Domain.Exceptions.Domains
{
    public class InvalidFormatDomainException : DomainException
    {
        public InvalidFormatDomainException(string message)
            : base(ResultErrorTypes.InvalidFomat, message)
        {
        }
    }
}
