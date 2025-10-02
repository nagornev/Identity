namespace Otp.Backgrounds.Abstractions.Processors
{
    public interface IBackgroundProcessor
    {
        Task StartAsync(CancellationToken cancellation = default);
    }
}
