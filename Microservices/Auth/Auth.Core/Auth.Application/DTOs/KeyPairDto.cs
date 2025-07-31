namespace Auth.Application.DTOs
{
    public class KeyPairDto
    {
        public KeyPairDto(Guid kid,
                          byte[] publicKey,
                          byte[] privateKey,
                          long createdAt,
                          long expiresAt)
        {
            Kid = kid;
            PublicKey = publicKey;
            PrivateKey = privateKey;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;
        }

        public Guid Kid { get; }

        public byte[] PublicKey { get; }

        public byte[] PrivateKey { get; }

        public long CreatedAt { get; }

        public long ExpiresAt { get; }
    }
}
