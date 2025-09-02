namespace Auth.Backgrounds.Abstractions.Processors
{
    public interface IBackgroundProcessor
    {
        Task StartAsync(CancellationToken cancellation);
    }
}
