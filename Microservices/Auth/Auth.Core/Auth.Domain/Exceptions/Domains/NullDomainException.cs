namespace Auth.Domain.Exceptions.Domains
{
    public class NullDomainException : DomainException
    {
        public NullDomainException(string message)
            : base(DomainErrorTypes.Null, message)
        {
        }
    }
}
