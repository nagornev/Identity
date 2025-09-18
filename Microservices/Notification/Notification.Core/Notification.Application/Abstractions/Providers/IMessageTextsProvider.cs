namespace Notification.Application.Abstractions.Providers
{
    public interface IMessageTextsProvider
    {
        string GetText(string notificationType, string text);
    }
}
