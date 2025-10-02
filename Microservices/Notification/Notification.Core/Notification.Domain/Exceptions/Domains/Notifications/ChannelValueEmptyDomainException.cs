namespace Notification.Domain.Exceptions.Domains.Notifications
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
