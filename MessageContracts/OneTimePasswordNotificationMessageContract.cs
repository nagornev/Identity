namespace MessageContracts
{
    public record OneTimePasswordNotificationMessageContract(Guid UserId, string OneTimePasswordValue, ChannelTypes ChannelType, string ChannelValue)
    {
    }
}
