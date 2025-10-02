using MessageContracts;

namespace Notification.Messaging.Abstractions.Providers
{
    public interface IChannelTypeProvider
    {
        string Get(ChannelTypes type);
    }
}
