namespace Auth.Keys.Options
{
    public class EmailStorageClientOptions : VaultStorageClientOptions
    {
        public EmailStorageClientOptions(string token, string address) : base(token, address)
        {
        }
    }
}
