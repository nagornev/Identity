namespace MessageContracts
{
    public class EmailAddressChangedMessageContract : IMessageContract
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
