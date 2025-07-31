namespace Auth.Domain.Exceptions.Domains.Sessions
{
    public class DeviceEmptyDomainException : EmptyDomainException
    {
        private const string _message = "The session device cannot be empty.";

        public DeviceEmptyDomainException()
            : base(_message)
        {
        }
    }
}
