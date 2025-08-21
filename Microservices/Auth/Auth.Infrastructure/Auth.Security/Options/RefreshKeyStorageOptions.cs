namespace Auth.Security.Options
{
    public class RefreshKeyStorageOptions : VaultKeyStorageOptions
    {
        public RefreshKeyStorageOptions(string primaryKey,
                                        string basePath)
             : base(primaryKey, basePath)
        {
        }
    }
}
