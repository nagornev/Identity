using Auth.Application.Abstractions.Storages;
using Auth.Keys.Abstractions.Providers;
using Auth.Keys.Options;
using Microsoft.Extensions.Options;

namespace Auth.Keys.Storages
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
