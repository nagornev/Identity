using VaultSharp;

namespace Auth.Security.Abstractions.Providers
{
    public interface IVaultStorageClientProvider : IStorageClientProvider
    {
        IVaultClient Create();
    }
}
