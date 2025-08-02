using Auth.Keys.Abstractions.Providers;
using Auth.Keys.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Keys.Providers
{
    public class EmailVaultClientProvider : VaultClientProvider<EmailStorageClientOptions>, IEmailClientProvider
    {
        public EmailVaultClientProvider(IOptions<EmailStorageClientOptions> vaultStorageClientOptions) : base(vaultStorageClientOptions)
        {
        }
    }
}
