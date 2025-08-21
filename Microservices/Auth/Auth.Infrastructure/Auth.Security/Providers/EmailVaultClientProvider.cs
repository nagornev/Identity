using Auth.Security.Abstractions.Providers;
using Auth.Security.Options;
using Microsoft.Extensions.Options;

namespace Auth.Security.Providers
{
    public class EmailVaultClientProvider : VaultClientProvider<EmailStorageClientOptions>, IChannelClientProvider
    {
        public EmailVaultClientProvider(IOptions<EmailStorageClientOptions> vaultStorageClientOptions) : base(vaultStorageClientOptions)
        {
        }
    }
}
