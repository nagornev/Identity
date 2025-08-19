using Auth.Application.Abstractions.Storages;
using Auth.Keys.Abstractions.Providers;
using Auth.Keys.Options;
using Microsoft.Extensions.Options;

namespace Auth.Keys.Storages
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
