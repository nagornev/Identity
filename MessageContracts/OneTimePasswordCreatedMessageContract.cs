namespace MessageContracts
{
    public class OneTimePasswordCreatedMessageContract : IMessageContract
    {
        public OneTimePasswordCreatedMessageContract(Guid oneTimePasswordId)
        {
            OneTimePasswordId = oneTimePasswordId;
        }

        public Guid OneTimePasswordId { get; }
    }
}
