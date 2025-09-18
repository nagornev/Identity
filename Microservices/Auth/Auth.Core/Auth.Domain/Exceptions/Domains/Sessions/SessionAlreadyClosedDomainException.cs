namespace Auth.Domain.Exceptions.Domains.Sessions
{
    public class SessionAlreadyClosedDomainException : AlreadyDomainException
    {
        private const string _message = "The session already closed.";

        public SessionAlreadyClosedDomainException()
            : base(_message)
        {
        }
    }
}
