using Auth.Security.Abstractions.Providers;
using Auth.Security.Options;
using Microsoft.Extensions.Options;

namespace Auth.Security.Providers
{
    public class RefreshVaultClientProvider : VaultClientProvider<RefreshStorageClientOptions>, IRefreshClientProvider
    {
        public RefreshVaultClientProvider(IOptions<RefreshStorageClientOptions> vaultStorageClientOptions) : base(vaultStorageClientOptions)
        {
        }
    }
}
