namespace Auth.Application.DTOs
{
    public class EmailToken
    {
        public EmailToken(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
