namespace Auth.Application.DTOs
{
    public class RefreshTokenPayload
    {
        public RefreshTokenPayload(Guid kid,
                                   Guid userId,
                                   Guid sessionId,
                                   Guid version,
                                   string publicKey)
        {
            Kid = kid;
            UserId = userId;
            SessionId = sessionId;
            Version = version;
            PublicKey = publicKey;
        }

        public Guid Kid { get; }

        public Guid UserId { get; }

        public Guid SessionId { get; }

        public Guid Version { get; }

        public string PublicKey { get; }
    }
}
