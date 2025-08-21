using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;
using Auth.Application.Options;
using Microsoft.Extensions.Options;

namespace Auth.Application.Services
{
    public class RefreshKeyRotationService : KeyRotationService<RefreshKeyOptions, IRefreshKeyStorage, IRefreshKeyPairFactory>, IRefreshKeyRotationService
    {
        public RefreshKeyRotationService(IRefreshKeyStorage keysStorage,
                                         IRefreshKeyPairFactory keysFactory,
                                         ITimeProvider timeProvider,
                                         IOptions<RefreshKeyOptions> keyOptions)
            : base(keysStorage, keysFactory, timeProvider, keyOptions)
        {
        }
    }
}
