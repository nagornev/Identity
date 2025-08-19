using Auth.Security.Abstractions.Providers;
using Auth.Security.Options;
using Microsoft.Extensions.Options;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;

namespace Auth.Security.Providers
{
    public abstract class VaultClientProvider<TVaultStorageClientOptions> : StorageClientProvider, IVaultStorageClientProvider
        where TVaultStorageClientOptions : VaultStorageClientOptions
    {
        protected readonly TVaultStorageClientOptions VaultStorageClientOptions;

        public VaultClientProvider(IOptions<TVaultStorageClientOptions> vaultStorageClientOptions)
        {
            VaultStorageClientOptions = vaultStorageClientOptions.Value;
        }
        public virtual IVaultClient Create()
        {
            TokenAuthMethodInfo authMethod = new TokenAuthMethodInfo(VaultStorageClientOptions.Token);
            VaultClientSettings settings = new VaultClientSettings(VaultStorageClientOptions.Address, authMethod);
            return new VaultClient(settings);
        }
    }
}
