namespace MessageContracts
{
    public class OneTimePasswordCreationCompleted : IMessageContract
    {
        public OneTimePasswordCreationCompleted(Guid oneTimePassordId)
        {
            OneTimePasswordId = oneTimePassordId;
        }

        public Guid OneTimePasswordId { get; }
    }
}
