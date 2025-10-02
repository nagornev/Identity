using Auth.Application.Abstractions.Services;
using Auth.Application.Options;
using Auth.Backgrounds.Abstractions.Processors;
using Microsoft.Extensions.Options;

namespace Auth.Backgrounds.Processors
{
    public class RefreshKeyRotationBackgroundProcessor : KeyRotationBackgroundProcessor<IRefreshKeyRotationService, RefreshKeyOptions>, IRefreshKeyRotationBackgroundProcesor
    {
        private const string _job = "rotate-refresh-key";

        public RefreshKeyRotationBackgroundProcessor(IServiceProvider serviceProvider, IOptions<RefreshKeyOptions> keyOptions)
            : base(_job, serviceProvider, keyOptions)
        {
        }
    }
}
