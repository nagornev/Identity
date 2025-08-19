namespace Auth.Keys.Options
{
    public class ChannelKeyStorageOptions : VaultKeyStorageOptions
    {
        public ChannelKeyStorageOptions(string primaryKey,
                                        string basePath)
            : base(primaryKey, basePath)
        {
        }
    }
}
