namespace Auth.Application.DTOs
{
    public class SignInOtpTokenPayload
    {
        public SignInOtpTokenPayload(Guid sessionId)
        {
            SessionId = sessionId;
        }

        public Guid SessionId { get; }
    }
}
