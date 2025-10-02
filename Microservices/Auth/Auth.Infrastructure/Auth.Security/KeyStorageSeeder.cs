using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Storages;
using Auth.Application.DTOs;

namespace Auth.Security
{
    public static class KeyStorageSeeder
    {
        public static async Task SeedKeyStorageAsync(IAccessKeyStorage accessKeyStorage, IAccessKeyPairFactory accessKeyPairFactory)
        {
            IReadOnlyCollection<KeyPair> keys = await accessKeyStorage.GetKeyPairsAsync();

            if (keys.Count < 1)
            {
                KeyPair key = accessKeyPairFactory.Create();
                await accessKeyStorage.SetPrimaryAsync(key);
            }
        }

        public static async Task SeedKeyStorageAsync(IRefreshKeyStorage refreshKeyStorage, IRefreshKeyPairFactory refreshKeyPairFactory)
        {
            IReadOnlyCollection<KeyPair> keys = await refreshKeyStorage.GetKeyPairsAsync();

            if (keys.Count < 1)
            {
                KeyPair key = refreshKeyPairFactory.Create();
                await refreshKeyStorage.SetPrimaryAsync(key);
            }
        }

        public static async Task SeedKeyStorageAsync(IChannelKeyStorage channelKeyStorage, IChannelKeyPairFactory channelKeyPairFactory)
        {
            IReadOnlyCollection<KeyPair> keys = await channelKeyStorage.GetKeyPairsAsync();

            if (keys.Count < 1)
            {
                KeyPair key = channelKeyPairFactory.Create();
                await channelKeyStorage.SetPrimaryAsync(key);
            }
        }
    }
}
