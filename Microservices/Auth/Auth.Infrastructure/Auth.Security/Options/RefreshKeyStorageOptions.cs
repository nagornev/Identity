namespace Auth.Security.Options
{
    public class RefreshKeyStorageOptions : VaultKeyStorageOptions
    {
        public RefreshKeyStorageOptions(string primaryKey, string mountPoint, string basePath) : base(primaryKey, mountPoint, basePath)
        {
        }
    }
}
