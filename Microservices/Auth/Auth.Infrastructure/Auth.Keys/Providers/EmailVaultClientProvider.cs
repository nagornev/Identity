using Auth.Keys.Abstractions.Providers;
using Auth.Keys.Options;
using Microsoft.Extensions.Options;

namespace Auth.Keys.Providers
{
    public class EmailVaultClientProvider : VaultClientProvider<EmailStorageClientOptions>, IChannelClientProvider
    {
        public EmailVaultClientProvider(IOptions<EmailStorageClientOptions> vaultStorageClientOptions) : base(vaultStorageClientOptions)
        {
        }
    }
}
