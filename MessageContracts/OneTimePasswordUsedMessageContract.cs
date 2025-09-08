namespace MessageContracts
{
    public class OneTimePasswordUsedMessageContract : IMessageContract
    {
        public OneTimePasswordUsedMessageContract(Guid oneTimePasswordId)
        {
            OneTimePasswordId = oneTimePasswordId;
        }

        public Guid OneTimePasswordId { get; }
    }
}
