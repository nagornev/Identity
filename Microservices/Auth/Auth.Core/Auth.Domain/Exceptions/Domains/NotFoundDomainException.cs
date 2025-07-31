namespace Auth.Domain.Exceptions.Domains
{
    public class NotFoundDomainException : DomainException
    {
        public NotFoundDomainException(string message)
            : base(DomainErrorTypes.NotFound, message)
        {
        }
    }
}
