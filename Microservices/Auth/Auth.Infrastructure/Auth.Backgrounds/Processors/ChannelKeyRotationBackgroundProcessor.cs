using Auth.Application.Abstractions.Services;
using Auth.Application.Options;
using Auth.Backgrounds.Abstractions.Processors;
using Microsoft.Extensions.Options;

namespace Auth.Backgrounds.Processors
{
    public class ChannelKeyRotationBackgroundProcessor : KeyRotationBackgroundProcessor<IChannelKeyRotationService, ChannelKeyOptions>, IChannelKeyRotationBackgroundProcessor
    {
        private const string _job = "rotate-channel-key";

        public ChannelKeyRotationBackgroundProcessor(IServiceProvider serviceProvider, IOptions<ChannelKeyOptions> keyOptions)
            : base(_job, serviceProvider, keyOptions)
        {
        }
    }
}
