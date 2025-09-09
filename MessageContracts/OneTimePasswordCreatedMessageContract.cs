namespace MessageContracts
{
    public class OneTimePasswordCreatedMessageContract
    {
        public OneTimePasswordCreatedMessageContract(Guid oneTimePasswordId)
        {
            OneTimePasswordId = oneTimePasswordId;
        }

        public Guid OneTimePasswordId { get; }
    }
}
