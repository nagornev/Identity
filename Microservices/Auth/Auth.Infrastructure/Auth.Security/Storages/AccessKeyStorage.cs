using Auth.Application.Abstractions.Storages;
using Auth.Security.Abstractions.Providers;
using Auth.Security.Options;
using Microsoft.Extensions.Options;

namespace Auth.Security.Storages
{
    public class AccessKeyStorage : VaultKeyStorage<IAccessClientProvider, AccessKeyStorageOptions>, IAccessKeyStorage
    {
        public AccessKeyStorage(IAccessClientProvider vaultStorageClientProvider,
                                IOptions<AccessKeyStorageOptions> keyStorageOptions)
            : base(vaultStorageClientProvider, keyStorageOptions)
        {
        }
    }
}
