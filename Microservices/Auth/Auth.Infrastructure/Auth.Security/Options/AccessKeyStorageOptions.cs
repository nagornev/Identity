namespace Auth.Security.Options
{
    public class AccessKeyStorageOptions : VaultKeyStorageOptions
    {
        public AccessKeyStorageOptions(string primaryKey, string mountPoint, string basePath) : base(primaryKey, mountPoint, basePath)
        {
        }
    }
}
