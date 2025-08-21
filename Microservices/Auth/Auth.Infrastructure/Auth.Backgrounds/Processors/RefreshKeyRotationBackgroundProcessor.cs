using Auth.Application.Abstractions.Services;
using Auth.Backgrounds.Abstractions.Processors;

namespace Auth.Backgrounds.Processors
{
    public class RefreshKeyRotationBackgroundProcessor : KeyRotationBackgroundProcessor<IRefreshKeyRotationService>, IRefreshKeyRotationBackgroundProcesor
    {
        public RefreshKeyRotationBackgroundProcessor(IRefreshKeyRotationService keyRotationService)
            : base(keyRotationService)
        {
        }
    }
}
