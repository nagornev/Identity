using Auth.Security.Abstractions.Providers;
using Auth.Security.Options;
using Microsoft.Extensions.Options;

namespace Auth.Security.Providers
{
    public class AccessVaultClientProvider : VaultClientProvider<AccessStorageClientOptions>, IAccessClientProvider
    {
        public AccessVaultClientProvider(IOptions<AccessStorageClientOptions> vaultStorageClientOptions)
            : base(vaultStorageClientOptions)
        {
        }
    }
}
