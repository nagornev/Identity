namespace Auth.Application.DTOs
{
    public class Otp
    {
        public Otp(Guid id,
                   string type,
                   string channel,
                   long expiresAt)
        {
            Id = id;
            Type = type;
            Channel = channel;
            ExpiresAt = expiresAt;
        }

        public Guid Id { get; }

        public string Type { get; }

        public string Channel { get; }

        public long ExpiresAt { get; }
    }
}
