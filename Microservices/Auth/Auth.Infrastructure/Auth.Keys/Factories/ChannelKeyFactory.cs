using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Options;
using Microsoft.Extensions.Options;

namespace Auth.Keys.Factories
{
    public class ChannelKeyFactory : RsaKeyFactory<ChannelKeyOptions>, IChannelKeyPairFactory
    {
        public ChannelKeyFactory(IOptions<ChannelKeyOptions> options,
                               ITimeProvider timeProvider)
            : base(options, timeProvider)
        {
        }
    }
}
