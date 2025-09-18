namespace Otp.Domain.Exceptions.Domains.Users
{
    public class ChannelValueEmptyDomainException : EmptyDomainException
    {
        private const string _message = "The channel value can`t be empty.";

        public ChannelValueEmptyDomainException()
            : base(_message)
        {
        }
    }
}
