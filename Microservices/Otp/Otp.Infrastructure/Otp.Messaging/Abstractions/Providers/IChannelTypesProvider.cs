using MessageContracts;

namespace Otp.Messaging.Abstractions.Providers
{
    public interface IChannelTypesProvider
    {
        ChannelTypes Get(string type);
    }
}
