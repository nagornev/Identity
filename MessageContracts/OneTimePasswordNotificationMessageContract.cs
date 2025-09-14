namespace MessageContracts
{
    public record OneTimePasswordNotificationMessageContract(Guid UserId, string OneTimePasswordValue, string Type, string Channel)
    {
    }
}
