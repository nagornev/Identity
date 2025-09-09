namespace MessageContracts
{
    public class UserCreatedMessageContract
    {
        public UserCreatedMessageContract(Guid userId,
                                          string emailAddress)
        {
            UserId = userId;
            EmailAddress = emailAddress;
        }

        public Guid UserId { get; }
        public string EmailAddress { get; }
    }
}
