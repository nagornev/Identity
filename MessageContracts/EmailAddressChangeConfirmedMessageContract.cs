namespace MessageContracts
{
    public class EmailAddressChangeConfirmedMessageContract : IMessageContract
    {
        public EmailAddressChangeConfirmedMessageContract(Guid userId,
                                                          string newEmailAddress)
        {
            UserId = userId;
            NewEmailAddress = newEmailAddress;
        }

        public Guid UserId { get; }

        public string NewEmailAddress { get; }
    }
}
