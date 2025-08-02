using Auth.Application.Abstractions.Storages;
using Auth.Keys.Abstractions.Providers;
using Auth.Keys.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaultSharp;

namespace Auth.Keys.Storages
{
    public class EmailKeyStorage : VaultKeyStorage<IEmailClientProvider, EmailKeyStorageOptions>, IEmailKeyStorage
    {
        public EmailKeyStorage(IEmailClientProvider vaultStorageClientProvider,
                               IOptions<EmailKeyStorageOptions> keyStorageOptions)
            : base(vaultStorageClientProvider, keyStorageOptions)
        {
        }
    }
}
