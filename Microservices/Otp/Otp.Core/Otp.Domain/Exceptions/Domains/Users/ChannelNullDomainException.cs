namespace Otp.Domain.Exceptions.Domains.Users
{
    public class ChannelNullDomainException : NullDomainException
    {
        private const string _message = "The channel can`t be null.";

        public ChannelNullDomainException()
            : base(_message)
        {
        }
    }
}
