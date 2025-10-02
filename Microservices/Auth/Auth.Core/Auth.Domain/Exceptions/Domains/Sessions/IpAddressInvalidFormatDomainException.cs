namespace Auth.Domain.Exceptions.Domains.Sessions
{
    public class IpAddressInvalidFormatDomainException : InvalidFormatDomainException
    {
        private const string _message = "The session ip address has an invalid format.";

        public IpAddressInvalidFormatDomainException()
            : base(_message)
        {
        }
    }
}
