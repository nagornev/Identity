namespace Auth.Application.DTOs
{
    public class RefreshTokenPayload
    {
        public RefreshTokenPayload(Guid userId,
                                   Guid sessionId,
                                   Guid version)
        {
            UserId = userId;
            SessionId = sessionId;
            Version = version;
        }

        public Guid UserId { get; }

        public Guid SessionId { get; }

        public Guid Version { get; }
    }
}
