namespace Auth.Application.DTOs
{
    public class OtpContent
    {
        public OtpContent(Guid userId,
                          string payload)
        {
            UserId = userId;
            Payload = payload;
        }

        public Guid UserId { get; }

        public string Payload { get; }
    }
}
