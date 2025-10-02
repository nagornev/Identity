using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Services
{
    public interface IEmailAddressChangeRequestService
    {
        Task<Otp> RequestAsync(Guid userId, string emailAddress, CancellationToken cancellation);
    }
}
