using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;

namespace Auth.Application.Services
{
    public class ChannelKeyRotationService : KeyRotationService<IChannelKeyStorage, IChannelKeyPairFactory>, IChannelKeyRotationService
    {
        public ChannelKeyRotationService(IChannelKeyStorage keysStorage,
                                         IChannelKeyPairFactory keysFactory)
            : base(keysStorage, keysFactory)
        {
        }
    }
}
