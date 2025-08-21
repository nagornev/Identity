using Auth.Application.Abstractions.Storages;
using Auth.Security.Abstractions.Providers;
using Auth.Security.Options;
using Microsoft.Extensions.Options;

namespace Auth.Security.Storages
{
    public class RefreshKeyStorage : VaultKeyStorage<IRefreshClientProvider, RefreshKeyStorageOptions>, IRefreshKeyStorage
    {
        public RefreshKeyStorage(IRefreshClientProvider vaultStorageClientProvider,
                                 IOptions<RefreshKeyStorageOptions> keyStorageOptions)
            : base(vaultStorageClientProvider, keyStorageOptions)
        {
        }
    }
}
