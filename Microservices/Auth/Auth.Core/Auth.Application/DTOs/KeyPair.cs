namespace Auth.Application.DTOs
{
    public class KeyPair
    {
        public KeyPair(Guid kid,
                       string algorithm,
                       byte[] privateKey,
                       byte[] publicKey,
                       long createdAt,
                       long expiresAt)
        {
            Kid = kid;
            Algorithm = algorithm;
            PrivateKey = privateKey;
            PublicKey = publicKey;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;
        }

        public Guid Kid { get; }

        public string Algorithm { get; }

        public byte[] PrivateKey { get; }

        public byte[] PublicKey { get; }

        public long CreatedAt { get; }

        public long ExpiresAt { get; }
    }
}
