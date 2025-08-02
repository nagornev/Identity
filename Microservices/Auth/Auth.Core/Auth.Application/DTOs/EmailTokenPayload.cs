namespace Auth.Application.DTOs
{
    public class EmailTokenPayload
    {
        public EmailTokenPayload(Guid kid,
                                 Guid userId)
        {
            Kid = kid;
            UserId = userId;
        }

        public Guid Kid { get; }

        public Guid UserId { get; }
    }
}
