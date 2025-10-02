using Otp.Application.DTOs;

namespace Otp.Application.Abstractions.Services
{
    public interface IOneTimeCreateService
    {
        Task<OneTimePasswordCreation> CreateAsync(Guid userId, string tag, string? payload = "", CancellationToken cancellation = default);
    }
}
