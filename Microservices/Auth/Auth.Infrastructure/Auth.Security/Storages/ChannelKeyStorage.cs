using Auth.Application.Abstractions.Storages;
using Auth.Security.Abstractions.Providers;
using Auth.Security.Options;
using Microsoft.Extensions.Options;

namespace Auth.Security.Storages
{
    public class ChannelKeyStorage : VaultKeyStorage<IChannelClientProvider, ChannelKeyStorageOptions>, IChannelKeyStorage
    {
        public ChannelKeyStorage(IChannelClientProvider vaultStorageClientProvider,
                                 IOptions<ChannelKeyStorageOptions> keyStorageOptions)
            : base(vaultStorageClientProvider, keyStorageOptions)
        {
        }
    }
}
