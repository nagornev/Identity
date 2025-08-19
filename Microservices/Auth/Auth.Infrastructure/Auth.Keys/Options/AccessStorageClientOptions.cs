namespace Auth.Keys.Options
{
    public class AccessStorageClientOptions : VaultStorageClientOptions
    {
        public AccessStorageClientOptions(string token, string address) : base(token, address)
        {
        }
    }
}
