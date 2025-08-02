using Auth.Application.Abstractions.Storages;
using Auth.Keys.Abstractions.Providers;
using Auth.Keys.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Keys.Storages
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
