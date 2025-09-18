namespace Notification.Domain.Exceptions.Domains.Notifications
{
    public class ChannelTypeEmptyDomainException : EmptyDomainException
    {
        private const string _message = "The channel type can`t be empty.";

        public ChannelTypeEmptyDomainException()
            : base(_message)
        {
        }
    }
}
