namespace Otp.Domain.Exceptions.Domains
{
    public abstract class NullDomainException : DomainException
    {
        public NullDomainException(string message)
            : base(DomainErrorTypes.Null, message)
        {
        }
    }
}
