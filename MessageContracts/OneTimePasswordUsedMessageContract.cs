namespace MessageContracts
{
    public class OneTimePasswordUsedMessageContract
    {
        public OneTimePasswordUsedMessageContract(Guid oneTimePasswordId)
        {
            OneTimePasswordId = oneTimePasswordId;
        }

        public Guid OneTimePasswordId { get; }
    }
}
