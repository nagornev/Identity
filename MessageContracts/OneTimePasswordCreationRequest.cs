namespace MessageContracts
{
    public record OneTimePasswordCreationRequest(Guid UserId, string Tag, string Payload)
    {
    }
}
