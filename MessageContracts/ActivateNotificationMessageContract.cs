namespace MessageContracts
{
    public record ActivateNotificationMessageContract(Guid UserId, string Url, ChannelTypes ChannelType, string ChannelValue)
    {
    }
}
