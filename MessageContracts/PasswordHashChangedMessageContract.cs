namespace MessageContracts
{
    public class PasswordHashChangedMessageContract : IMessageContract
    {
        public PasswordHashChangedMessageContract(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
