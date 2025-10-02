namespace MessageContracts
{
    public record OneTimePasswordValidationCompleted(bool IsValid, Guid UserId, string Payload)
    {
    }
}
