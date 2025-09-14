namespace MessageContracts
{
    public record OneTimePasswordValidationRequest(Guid OneTimePasswordId, string OneTimePasswordValue, string Tag)
    {
    }
}
