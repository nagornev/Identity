namespace MessageContracts
{
    public class OneTimePasswordCreationCompleted
    {
        public OneTimePasswordCreationCompleted(Guid oneTimePassordId)
        {
            OneTimePasswordId = oneTimePassordId;
        }

        public Guid OneTimePasswordId { get; }
    }
}
