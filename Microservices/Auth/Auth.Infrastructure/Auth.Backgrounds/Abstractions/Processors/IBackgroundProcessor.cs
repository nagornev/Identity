namespace Auth.Backgrounds.Abstractions.Processors
{
    public interface IBackgroundProcessor
    {
        Task HandleAsync(CancellationToken cancellation);
    }
}
