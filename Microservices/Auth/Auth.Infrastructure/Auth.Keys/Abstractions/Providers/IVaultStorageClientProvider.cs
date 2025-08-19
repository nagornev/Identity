using VaultSharp;

namespace Auth.Keys.Abstractions.Providers
{
    public interface IVaultStorageClientProvider : IStorageClientProvider
    {
        IVaultClient Create();
    }
}
