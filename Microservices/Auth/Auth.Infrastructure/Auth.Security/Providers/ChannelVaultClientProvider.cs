using Auth.Security.Abstractions.Providers;
using Auth.Security.Options;
using Microsoft.Extensions.Options;

namespace Auth.Security.Providers
{
    public class ChannelVaultClientProvider : VaultClientProvider<ChannelStorageClientOptions>, IChannelClientProvider
    {
        public ChannelVaultClientProvider(IOptions<ChannelStorageClientOptions> vaultStorageClientOptions) : base(vaultStorageClientOptions)
        {
        }
    }
}
