namespace Auth.Domain.Exceptions.Domains
{
    public class UnconfirmedDomainException : DomainException
    {
        public UnconfirmedDomainException(string message)
            : base(DomainErrorTypes.Unconfirmed, message)
        {
        }
    }
}
