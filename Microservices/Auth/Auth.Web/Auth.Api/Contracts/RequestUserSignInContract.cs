namespace Auth.Api.Contracts
{
    public class RequestUserSignInContract
    {
        public RequestUserSignInContract(string emailAddress,
                                         string password,
                                         string audience,
                                         string publicKey)
        {
            EmailAddress = emailAddress;
            Password = password;
            Audience = audience;
            PublicKey = publicKey;
        }

        public string EmailAddress { get; }

        public string Password { get; }

        public string Audience { get; }

        public string PublicKey { get; }
    }
}
