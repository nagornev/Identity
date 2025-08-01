namespace Auth.Application.Features.Logout
{
    public class LogoutCommand : ResultRequest
    {
        public LogoutCommand(Guid sessionId)
        {
            SessionId = sessionId;
        }

        public Guid SessionId { get; }
    }
}
