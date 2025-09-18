namespace Notification.Application.Abstractions.Providers
{
    public interface IMessageTextProvider
    {
        string GetHandableType();

        string GetText(string text);
    }
}
