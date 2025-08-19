using Auth.Keys.Abstractions.Providers;
using Auth.Keys.Options;
using Microsoft.Extensions.Options;

namespace Auth.Keys.Providers
{
    public class RefreshVaultClientProvider : VaultClientProvider<RefreshStorageClientOptions>, IRefreshClientProvider
    {
        public RefreshVaultClientProvider(IOptions<RefreshStorageClientOptions> vaultStorageClientOptions) : base(vaultStorageClientOptions)
        {
        }
    }
}
