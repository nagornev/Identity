using MessageContracts;
using Otp.Domain.Consts;
using Otp.Messaging.Abstractions.Providers;

namespace Otp.Messaging.Providers
{
    public class ChannelTypesProvider : IChannelTypesProvider
    {
        private readonly IReadOnlyDictionary<string, ChannelTypes> _channelTypes = new Dictionary<string, ChannelTypes>
        {
            {ChannelType.Email, ChannelTypes.Email},
            {ChannelType.Sms, ChannelTypes.Sms},
        };

        public ChannelTypes Get(string type)
        {
            return _channelTypes.TryGetValue(type, out var channelType) ?
                    channelType :
                    throw new NotSupportedException($"This channel type ({type}) is not supported.");
        }
    }
}
