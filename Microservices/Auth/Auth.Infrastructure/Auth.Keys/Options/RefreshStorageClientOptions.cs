namespace Auth.Keys.Options
{
    public class RefreshStorageClientOptions : VaultStorageClientOptions
    {
        public RefreshStorageClientOptions(string token, string address) : base(token, address)
        {
        }
    }
}
