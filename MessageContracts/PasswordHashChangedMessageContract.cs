namespace MessageContracts
{
    public class PasswordHashChangedMessageContract
    {
        public PasswordHashChangedMessageContract(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
