namespace MessageContracts
{
    public record ChannelNotificationMessageContract(Guid UserId, string Url, ChannelTypes ChannelType, string ChannelValue)
    {
    }
}
