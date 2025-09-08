using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;

namespace Auth.Application.Services
{
    public class RefreshKeyRotationService : KeyRotationService<IRefreshKeyStorage, IRefreshKeyPairFactory>, IRefreshKeyRotationService
    {
        public RefreshKeyRotationService(IRefreshKeyStorage keysStorage,
                                         IRefreshKeyPairFactory keysFactory)
            : base(keysStorage, keysFactory)
        {
        }
    }
}
