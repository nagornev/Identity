namespace Notification.Domain.Exceptions.Domains.Notifications
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
