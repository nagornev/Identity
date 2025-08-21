using Auth.Application.Abstractions.Services;
using Auth.Backgrounds.Abstractions.Processors;

namespace Auth.Backgrounds.Processors
{
    public class ChannelKeyRotationBackgroundProcessor : KeyRotationBackgroundProcessor<IChannelKeyRotationService>, IChannelKeyRotationBackgroundProcessor
    {
        public ChannelKeyRotationBackgroundProcessor(IChannelKeyRotationService keyRotationService)
            : base(keyRotationService)
        {
        }
    }
}
