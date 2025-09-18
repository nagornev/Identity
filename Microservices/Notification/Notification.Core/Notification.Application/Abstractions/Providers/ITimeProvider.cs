namespace Notification.Application.Abstractions.Providers
{
    public interface ITimeProvider
    {
        long NowUnix();

        DateTime NowDateTime();
    }
}
