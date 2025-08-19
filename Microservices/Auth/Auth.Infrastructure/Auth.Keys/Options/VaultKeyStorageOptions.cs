namespace Auth.Keys.Options
{
    public abstract class VaultKeyStorageOptions : KeyStorageOptions
    {
        public VaultKeyStorageOptions(string primaryKey,
                                      string basePath)
        {
            PrimaryKey = primaryKey;
            BasePath = basePath;
        }

        public string PrimaryKey { get; }

        public string BasePath { get; }
    }
}
