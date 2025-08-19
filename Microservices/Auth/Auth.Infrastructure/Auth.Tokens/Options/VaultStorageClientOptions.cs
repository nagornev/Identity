namespace Auth.Security.Options
{
    public class VaultStorageClientOptions : StorageClientOptions
    {
        public VaultStorageClientOptions(string token,
                                         string address)
        {
            Token = token;
            Address = address;
        }

        public string Token { get; }

        public string Address { get; }
    }
}
