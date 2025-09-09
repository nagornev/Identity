namespace MessageContracts
{
    public class UserDeletedMessageContract
    {
        public UserDeletedMessageContract(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
