namespace Auth.Security.Options
{
    public class AccessKeyStorageOptions : VaultKeyStorageOptions
    {
        public AccessKeyStorageOptions(string primaryKey,
                                       string basePath)
            : base(primaryKey, basePath)
        {
        }
    }
}
