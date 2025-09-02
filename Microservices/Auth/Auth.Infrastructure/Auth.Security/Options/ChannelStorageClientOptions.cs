namespace Auth.Security.Options
{
    public class ChannelStorageClientOptions : VaultStorageClientOptions
    {
        public ChannelStorageClientOptions(string token, string address) : base(token, address)
        {
        }
    }
}
