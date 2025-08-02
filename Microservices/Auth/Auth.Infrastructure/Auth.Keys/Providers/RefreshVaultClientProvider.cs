using Auth.Application.Options;
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
    public class RefreshVaultClientProvider : VaultClientProvider<RefreshStorageClientOptions>, IRefreshClientProvider
    {
        public RefreshVaultClientProvider(IOptions<RefreshStorageClientOptions> vaultStorageClientOptions) : base(vaultStorageClientOptions)
        {
        }
    }
}
