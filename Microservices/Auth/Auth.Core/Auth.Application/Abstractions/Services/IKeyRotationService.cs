namespace Auth.Application.Abstractions.Services
{
    public interface IKeyRotationService
    {
        Task<TimeSpan> RotateAsync(CancellationToken cancellation = default);
    }
}
