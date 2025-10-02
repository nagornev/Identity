namespace Notification.Application.Abstractions.Senders
{
    public interface IMessageSender
    {
        string GetHandableType();

        Task SendAsync(string channel, string text, CancellationToken cancellation = default);
    }
}
