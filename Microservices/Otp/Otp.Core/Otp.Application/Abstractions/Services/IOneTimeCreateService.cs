namespace Otp.Application.Abstractions.Services
{
    public interface IOneTimeCreateService
    {
        Task<Guid> CreateAsync(string tag, Guid subject, string? payload = "", CancellationToken cancellation = default);
    }
}
