namespace MessageContracts
{
    public class UserDeletedMessageContract : IMessageContract
    {
        public UserDeletedMessageContract(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
