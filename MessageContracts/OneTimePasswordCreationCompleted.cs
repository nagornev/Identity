namespace MessageContracts
{
    public record OneTimePasswordCreationCompleted(Guid OneTimePasswordId, string Type, string Channel, long ExpiresAt)
    {
    }
}
