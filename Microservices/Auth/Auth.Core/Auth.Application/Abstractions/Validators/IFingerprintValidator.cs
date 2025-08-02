using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Validators
{
    public interface IFingerprintValidator
    {
        Task<bool> ValidateAsync(FingerprintValidationParameters parameters, string publicKey, CancellationToken cancellation = default);
    }
}
