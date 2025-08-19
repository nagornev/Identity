namespace Auth.Application.DTOs
{
    public class OtpContent
    {
        public OtpContent(Guid subject,
                          string payload)
        {
            Subject = subject;
            Payload = payload;
        }

        public Guid Subject { get; }

        public string Payload { get; }
    }
}
