namespace Auth.Backgrounds.Abstractions.Processors
{
    public interface IOutboxProcessor
    {
        Task HandleAsync(CancellationToken cancellation);
    }
}
