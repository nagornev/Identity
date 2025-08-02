using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaultSharp;

namespace Auth.Keys.Abstractions.Providers
{
    public interface IVaultStorageClientProvider : IStorageClientProvider
    {
        IVaultClient Create();
    }
}
