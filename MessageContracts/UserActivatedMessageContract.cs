namespace MessageContracts
{
    public class UserActivatedMessageContract
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
