using Auth.Application.Abstractions.Services;
using Auth.Backgrounds.Abstractions.Processors;

namespace Auth.Backgrounds.Processors
{
    public class AccessKeysRotationBackgroundProcessor : KeyRotationBackgroundProcessor<IAccessKeyRotationService>, IAccessKeyRotationBackroundProcessor
    {
        public AccessKeysRotationBackgroundProcessor(IAccessKeyRotationService keyRotationService)
            : base(keyRotationService)
        {
        }
    }
}
