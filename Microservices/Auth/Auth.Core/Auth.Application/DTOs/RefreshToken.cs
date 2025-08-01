namespace Auth.Application.DTOs
{
    public class RefreshToken
    {
        public RefreshToken(Guid id,
                            Guid userId,
                            Guid sessionId,
                            Guid version,
                            string publicKey)
        {
            Id = id;
            UserId = userId;
            SessionId = sessionId;
            Version = version;
            PublicKey = publicKey;
        }

        public Guid Id { get; }

        public Guid UserId { get; }

        public Guid SessionId { get; }

        public Guid Version { get; }

        public string PublicKey { get; }
    }
}
