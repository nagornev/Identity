namespace Auth.Security.Options
{
    public abstract class VaultKeyStorageOptions : KeyStorageOptions
    {
        public VaultKeyStorageOptions(string primaryKey,
                                      string mountPoint,
                                      string basePath)
        {
            PrimaryKey = primaryKey;
            MountPoint = mountPoint;
            BasePath = basePath;
        }

        public string PrimaryKey { get; }

        public string MountPoint { get; }

        public string BasePath { get; }
    }
}
