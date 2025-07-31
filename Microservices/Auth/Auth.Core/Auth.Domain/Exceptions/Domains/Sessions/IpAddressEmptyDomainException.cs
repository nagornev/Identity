namespace Auth.Domain.Exceptions.Domains.Sessions
{
    public class IpAddressEmptyDomainException : EmptyDomainException
    {
        private const string _message = "The user`s ip address cannot be empty.";

        public IpAddressEmptyDomainException()
            : base(_message)
        {
        }
    }
}
