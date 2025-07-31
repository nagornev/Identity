namespace Auth.Domain.Exceptions.Domains
{
    public class OutOfRangeDomainException : DomainException
    {
        public OutOfRangeDomainException(string message)
            : base(DomainErrorTypes.OutOfRange, message)
        {
        }
    }
}
