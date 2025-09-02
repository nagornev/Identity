using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;
using Auth.Application.Options;
using Microsoft.Extensions.Options;

namespace Auth.Application.Services
{
    public class AccessKeyRotationService : KeyRotationService<IAccessKeyStorage, IAccessKeyPairFactory>, IAccessKeyRotationService
    {
        public AccessKeyRotationService(IAccessKeyStorage keysStorage,
                                        IAccessKeyPairFactory keysFactory)
            : base(keysStorage, keysFactory)
        {
        }
    }
}
