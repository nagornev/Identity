namespace MessageContracts
{
    public record ChannelNotificationMessageContract(Guid UserId, string Channel, string Token)
    {
    }
}
