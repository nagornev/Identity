using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Services
{
    public interface IPasswordChangeRequestService
    {
        Task<Otp> RequestAsync(Guid userId, string oldPassword, string newPassword, CancellationToken cancellation = default);
    }
}
