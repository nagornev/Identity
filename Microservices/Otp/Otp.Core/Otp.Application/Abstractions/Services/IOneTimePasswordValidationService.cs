using Otp.Application.DTOs;

namespace Otp.Application.Abstractions.Services
{
    public interface IOneTimePasswordValidationService
    {
        Task<OneTimePasswordValidation> ValidateAsync(Guid oneTimePasswordId, string oneTimePasswordValue, string tag, CancellationToken cancellation = default);
    }
}
