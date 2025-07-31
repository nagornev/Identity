namespace Auth.Application.Features.LogoutAll
{
    public class LogoutAllCommand : ResultRequest
    {
        public LogoutAllCommand(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
