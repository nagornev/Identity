namespace MessageContracts
{
    public class EmailAddressChangedMessageContract
    {
        public EmailAddressChangedMessageContract(Guid userId, string emailAddress)
        {
            UserId = userId;
            EmailAddress = emailAddress;
        }

        public Guid UserId { get; }

        public string EmailAddress { get; }
    }
}
