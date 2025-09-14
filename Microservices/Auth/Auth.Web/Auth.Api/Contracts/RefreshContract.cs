namespace Auth.Api.Contracts
{
    public class RefreshContract
    {
        public RefreshContract(string refreshToken,
                               string newPublicKey,
                               long timestamp,
                               string signature)
        {
            RefreshToken = refreshToken;
            NewPublicKey = newPublicKey;
            Timestamp = timestamp;
            Signature = signature;
        }

        public string RefreshToken { get; }

        public string NewPublicKey { get; }

        public long Timestamp { get; }

        public string Signature { get; }
    }
}
