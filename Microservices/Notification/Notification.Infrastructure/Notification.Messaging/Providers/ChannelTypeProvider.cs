using MessageContracts;
using Notification.Domain.Consts;
using Notification.Messaging.Abstractions.Providers;

namespace Notification.Messaging.Providers
{
    public class ChannelTypeProvider : IChannelTypeProvider
    {
        private readonly IReadOnlyDictionary<ChannelTypes, string> _channelType = new Dictionary<ChannelTypes, string>
        {
            { ChannelTypes.Email, ChannelType.Email},
            { ChannelTypes.Sms, ChannelType.Sms},
        };

        public string Get(ChannelTypes type)
        {
            return _channelType.TryGetValue(type, out var channelType) ?
                    channelType :
                    throw new NotSupportedException($"This channel type ({type}) is not supported.");
        }
    }
}
