namespace Notification.Domain.Exceptions.Domains
{
    public class AlreadyDomainException : DomainException
    {
        public AlreadyDomainException(string message)
            : base(DomainErrorTypes.Already, message)
        {
        }
    }
}
