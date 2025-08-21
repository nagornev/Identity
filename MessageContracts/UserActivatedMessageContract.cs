namespace MessageContracts
{
    public class UserActivatedMessageContract : IMessageContract
    {
        public UserActivatedMessageContract(Guid userId,
                                            string emailAddress)
        {
            UserId = userId;
            EmailAddress = emailAddress;
        }

        public Guid UserId { get; }

        public string EmailAddress { get; }
    }
}
