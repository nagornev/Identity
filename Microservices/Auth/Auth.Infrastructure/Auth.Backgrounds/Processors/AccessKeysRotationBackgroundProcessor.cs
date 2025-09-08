using Auth.Application.Abstractions.Services;
using Auth.Application.Options;
using Auth.Backgrounds.Abstractions.Processors;
using Microsoft.Extensions.Options;

namespace Auth.Backgrounds.Processors
{
    public class AccessKeysRotationBackgroundProcessor : KeyRotationBackgroundProcessor<IAccessKeyRotationService, AccessKeyOptions>, IAccessKeyRotationBackroundProcessor
    {
        private const string _job = "rotate-access-key";

        public AccessKeysRotationBackgroundProcessor(IServiceProvider serviceProvider, IOptions<AccessKeyOptions> keyOptions)
            : base(_job, serviceProvider, keyOptions)
        {
        }
    }
}
