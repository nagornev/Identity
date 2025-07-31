namespace Auth.Domain.Exceptions.Domains
{
    public class InvalidFormatDomainException : DomainException
    {
        public InvalidFormatDomainException(string message)
            : base(DomainErrorTypes.InvalidFomat, message)
        {
        }
    }
}
