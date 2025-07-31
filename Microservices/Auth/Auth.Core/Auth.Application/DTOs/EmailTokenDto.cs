namespace Auth.Application.DTOs
{
    public class EmailTokenDto
    {
        public EmailTokenDto(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
