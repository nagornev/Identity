namespace Auth.Security.Options
{
    public class ChannelKeyStorageOptions : VaultKeyStorageOptions
    {
        public ChannelKeyStorageOptions(string primaryKey, string mountPoint, string basePath) : base(primaryKey, mountPoint, basePath)
        {
        }
    }
}
