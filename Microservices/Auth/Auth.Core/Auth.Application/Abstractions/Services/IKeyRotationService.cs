namespace Auth.Application.Abstractions.Services
{
    public interface IKeyRotationService
    {
        Task RotateAsync(CancellationToken cancellation = default);
    }
}
